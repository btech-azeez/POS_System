namespace POS_System
{
    partial class POSForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewProducts = new System.Windows.Forms.DataGridView();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.listViewCart = new System.Windows.Forms.ListView();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblTax = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).BeginInit();
            this.SuspendLayout();

            // dataGridViewProducts
            this.dataGridViewProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProducts.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewProducts.Name = "dataGridViewProducts";
            this.dataGridViewProducts.Size = new System.Drawing.Size(400, 200);

            // txtQuantity
            this.txtQuantity.Location = new System.Drawing.Point(12, 230);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(100, 22);

            // btnAddToCart
            this.btnAddToCart.Location = new System.Drawing.Point(130, 230);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(100, 23);
            this.btnAddToCart.Text = "Add to Cart";
            this.btnAddToCart.Click += new System.EventHandler(this.BtnAddToCart_Click);

            // listViewCart
            this.listViewCart.Location = new System.Drawing.Point(12, 270);
            this.listViewCart.Name = "listViewCart";
            this.listViewCart.Size = new System.Drawing.Size(400, 150);

            // txtDiscount
            this.txtDiscount.Location = new System.Drawing.Point(12, 440);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(100, 22);

            // btnCheckout
            this.btnCheckout.Location = new System.Drawing.Point(130, 440);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(100, 23);
            this.btnCheckout.Text = "Checkout";
            this.btnCheckout.Click += new System.EventHandler(this.BtnCheckout_Click);

            // Labels
            this.lblDiscount.Location = new System.Drawing.Point(250, 440);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(150, 23);

            this.lblTax.Location = new System.Drawing.Point(250, 470);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(150, 23);

            this.lblTotal.Location = new System.Drawing.Point(250, 500);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(150, 23);

            // POSForm
            this.ClientSize = new System.Drawing.Size(450, 550);
            this.Controls.Add(this.dataGridViewProducts);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.btnAddToCart);
            this.Controls.Add(this.listViewCart);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.btnCheckout);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.lblTax);
            this.Controls.Add(this.lblTotal);
            this.Name = "POSForm";
            this.Text = "POS System";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dataGridViewProducts;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button btnAddToCart;
        private System.Windows.Forms.ListView listViewCart;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.Label lblTotal;
    }
}
