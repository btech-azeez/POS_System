using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_System
{
    public partial class POSForm : Form
    {
        private List<CartItem> cartItems = new List<CartItem>();
        private string connectionString = ConfigurationManager.ConnectionStrings["POS_DBConnection"].ConnectionString;

        public POSForm()
        {
            InitializeComponent();
            LoadProducts();
            StyleUI();
        }

        private void LoadProducts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT ProductID, ProductName, Category, Price, Discount, Tax, IsDiscountPercentage FROM Products", conn))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridViewProducts.DataSource = dt;
            }
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product.");
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Enter a valid quantity.");
                return;
            }

            DataGridViewRow row = dataGridViewProducts.SelectedRows[0];
            int productId = Convert.ToInt32(row.Cells["ProductID"].Value);
            decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
            decimal discount = Convert.ToDecimal(row.Cells["Discount"].Value);
            decimal tax = Convert.ToDecimal(row.Cells["Tax"].Value);
            bool isDiscountPercentage = Convert.ToBoolean(row.Cells["IsDiscountPercentage"].Value);

            var existingItem = cartItems.FirstOrDefault(i => i.ProductID == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cartItems.Add(new CartItem
                {
                    ProductID = productId,
                    ProductName = row.Cells["ProductName"].Value.ToString(),
                    Price = price,
                    Quantity = quantity,
                    Discount = discount,
                    Tax = tax,
                    IsDiscountPercentage = isDiscountPercentage
                });
            }
            UpdateCartDisplay();
        }

        private void UpdateCartDisplay()
        {
            listViewCart.Items.Clear();
            decimal subtotal = 0, totalDiscount = 0, totalTax = 0;

            foreach (var item in cartItems)
            {
                decimal itemTotal = item.Price * item.Quantity;
                decimal itemDiscount = item.IsDiscountPercentage
                    ? itemTotal * (item.Discount / 100) // Percentage-based discount
                    : item.Discount * item.Quantity;   // Fixed discount per item

                decimal itemTax = (itemTotal - itemDiscount) * (item.Tax / 100);

                subtotal += itemTotal;
                totalDiscount += itemDiscount;
                totalTax += itemTax;

                ListViewItem listItem = new ListViewItem(item.ProductName);
                listItem.SubItems.Add(item.Quantity.ToString());
                listItem.SubItems.Add(item.Price.ToString("C"));
                listItem.SubItems.Add((itemTotal - itemDiscount).ToString("C"));
                listViewCart.Items.Add(listItem);
            }

            decimal total = subtotal - totalDiscount + totalTax;

            lblDiscount.Text = $"Discount: {totalDiscount:C}";
            lblTax.Text = $"Tax: {totalTax:C}";
            lblTotal.Text = $"Total: {total:C}";
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (cartItems.Count == 0)
            {
                MessageBox.Show("Your cart is empty.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        decimal subtotal = cartItems.Sum(i => i.Price * i.Quantity);
                        decimal totalDiscount = cartItems.Sum(i => i.IsDiscountPercentage
                            ? (i.Price * i.Quantity * (i.Discount / 100))
                            : (i.Discount * i.Quantity));
                        decimal totalTax = cartItems.Sum(i => (i.Price * i.Quantity - (i.IsDiscountPercentage
                            ? (i.Price * i.Quantity * (i.Discount / 100))
                            : (i.Discount * i.Quantity))) * (i.Tax / 100));
                        decimal totalAmount = subtotal - totalDiscount + totalTax;

                        // ✅ Insert into Orders table
                        using (SqlCommand cmd = new SqlCommand(
                            "INSERT INTO Orders (Subtotal, TotalDiscount, TotalTax, TotalAmount, FinalAmount) " +
                            "OUTPUT INSERTED.OrderID VALUES (@Subtotal, @TotalDiscount, @TotalTax, @TotalAmount, @FinalAmount)",
                            conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Subtotal", subtotal);
                            cmd.Parameters.AddWithValue("@TotalDiscount", totalDiscount);
                            cmd.Parameters.AddWithValue("@TotalTax", totalTax);
                            cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                            cmd.Parameters.AddWithValue("@FinalAmount", totalAmount); // Ensure FinalAmount is set

                            int orderId = (int)cmd.ExecuteScalar(); // Get OrderID

                            // ✅ Insert into OrderDetails table
                            foreach (var item in cartItems)
                            {
                                using (SqlCommand cmdDetail = new SqlCommand(
                                    "INSERT INTO OrderDetails (OrderID, ProductID, Quantity, Price, Discount, Tax, FinalTotal) " +
                                    "VALUES (@OrderID, @ProductID, @Quantity, @Price, @Discount, @Tax, @FinalTotal)",
                                    conn, transaction))
                                {
                                    decimal itemTotal = item.Price * item.Quantity;
                                    decimal itemDiscount = item.IsDiscountPercentage
                                        ? itemTotal * (item.Discount / 100)
                                        : item.Discount * item.Quantity;
                                    decimal itemTax = (itemTotal - itemDiscount) * (item.Tax / 100);
                                    decimal finalTotal = itemTotal - itemDiscount + itemTax;

                                    cmdDetail.Parameters.AddWithValue("@OrderID", orderId);
                                    cmdDetail.Parameters.AddWithValue("@ProductID", item.ProductID);
                                    cmdDetail.Parameters.AddWithValue("@Quantity", item.Quantity);
                                    cmdDetail.Parameters.AddWithValue("@Price", item.Price);
                                    cmdDetail.Parameters.AddWithValue("@Discount", item.Discount);
                                    cmdDetail.Parameters.AddWithValue("@Tax", item.Tax);
                                    cmdDetail.Parameters.AddWithValue("@FinalTotal", finalTotal);

                                    cmdDetail.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                        }

                        MessageBox.Show("Order placed successfully!");
                        cartItems.Clear();
                        UpdateCartDisplay();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error placing order: " + ex.Message);
                    }
                }
            }
        }
        private void StyleUI()
        {
            this.BackColor = Color.LightGray;
            btnAddToCart.BackColor = Color.LightBlue;
            btnCheckout.BackColor = Color.LightGreen;
            btnAddToCart.Font = new Font("Arial", 10, FontStyle.Bold);
            btnCheckout.Font = new Font("Arial", 10, FontStyle.Bold);
            lblDiscount.Font = new Font("Arial", 10, FontStyle.Bold);
            lblTax.Font = new Font("Arial", 10, FontStyle.Bold);
            lblTotal.Font = new Font("Arial", 12, FontStyle.Bold);
        }
    }

    public class CartItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public bool IsDiscountPercentage { get; set; }
    }
}
