using DataStrucutre.DataStructure;
using DataStrucutre.Enums;
using DataStrucutre.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Admin
{
    public partial class Form1 : Form
    {
        private static readonly Color AppBackground = Color.FromArgb(244, 247, 251);
        private static readonly Color PanelBackground = Color.White;
        private static readonly Color HeaderBackground = Color.FromArgb(31, 78, 121);
        private static readonly Color PrimaryAction = Color.FromArgb(25, 74, 122);
        private static readonly Color TextPrimary = Color.FromArgb(17, 24, 39);
        private static readonly Color TextSecondary = Color.FromArgb(100, 116, 139);
        private static readonly Color Danger = Color.FromArgb(190, 24, 24);
        private static readonly Color Warning = Color.FromArgb(180, 83, 9);
        private static readonly Color Success = Color.FromArgb(21, 128, 61);

        private readonly InventoryService inventoryService;
        private readonly BindingSource productBindingSource;

        private UserAccount currentUser;
        private TextBox txtLoginEmail;
        private TextBox txtLoginPassword;
        private Label lblLoginStatus;

        private Label lblTotalProducts;
        private Label lblTotalStock;
        private Label lblLowStock;
        private Label lblInventoryValue;
        private Label lblStatus;
        private Label lblDataFile;

        private TextBox txtSearch;
        private ComboBox cmbCategoryFilter;
        private DataGridView gridProducts;

        private string selectedProductId;
        private TextBox txtProductId;
        private TextBox txtSku;
        private TextBox txtProductName;
        private TextBox txtSupplier;
        private ComboBox cmbProductCategory;
        private NumericUpDown numOriginalPrice;
        private NumericUpDown numOffer;
        private NumericUpDown numTax;
        private NumericUpDown numStock;
        private NumericUpDown numLowStock;
        private Label lblEditorStatus;

        public Form1()
        {
            inventoryService = new InventoryService();
            productBindingSource = new BindingSource();

            InitializeComponent();
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BackColor = AppBackground;
            BuildLoginView();
        }

        private void BuildLoginView()
        {
            SuspendLayout();
            Controls.Clear();
            AcceptButton = null;

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = AppBackground,
                Padding = new Padding(28),
                ColumnCount = 2,
                RowCount = 1
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));

            layout.Controls.Add(CreateLoginHero(), 0, 0);
            layout.Controls.Add(CreateLoginPanel(), 1, 0);

            Controls.Add(layout);
            ResumeLayout(true);
        }

        private Control CreateLoginHero()
        {
            TableLayoutPanel hero = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = HeaderBackground,
                Padding = new Padding(34),
                RowCount = 3,
                ColumnCount = 1
            };
            hero.RowStyles.Add(new RowStyle(SizeType.Absolute, 88F));
            hero.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            hero.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));

            Label brand = new Label
            {
                Dock = DockStyle.Fill,
                Text = "StockFlow",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 28F, FontStyle.Bold, GraphicsUnit.Point),
                TextAlign = ContentAlignment.MiddleLeft
            };

            PictureBox picture = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = HeaderBackground
            };
            Image heroImage = LoadImage("inventoryLogin.png");
            if (heroImage != null)
            {
                picture.Image = heroImage;
            }

            Label copy = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Track products, stock levels, pricing, suppliers, and low-stock alerts from one desktop dashboard.",
                ForeColor = Color.FromArgb(226, 232, 240),
                Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point),
                TextAlign = ContentAlignment.MiddleLeft
            };

            hero.Controls.Add(brand, 0, 0);
            hero.Controls.Add(picture, 0, 1);
            hero.Controls.Add(copy, 0, 2);

            return hero;
        }

        private Control CreateLoginPanel()
        {
            Panel outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = PanelBackground,
                Padding = new Padding(58, 78, 58, 58)
            };

            TableLayoutPanel panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 9
            };
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            Label title = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Sign in",
                ForeColor = TextPrimary,
                Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label subtitle = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Use the default admin account to start managing inventory.",
                ForeColor = TextSecondary,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point),
                TextAlign = ContentAlignment.MiddleLeft
            };

            txtLoginEmail = CreateTextBox("admin@stockflow.local");
            txtLoginPassword = CreateTextBox("admin123");
            txtLoginPassword.UseSystemPasswordChar = true;

            Button btnLogin = CreateButton("Login", PrimaryAction);
            btnLogin.Height = 42;
            btnLogin.Click += LoginClick;
            AcceptButton = btnLogin;

            lblLoginStatus = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Default: admin@stockflow.local / admin123",
                ForeColor = TextSecondary,
                TextAlign = ContentAlignment.TopLeft
            };

            panel.Controls.Add(title, 0, 0);
            panel.Controls.Add(subtitle, 0, 1);
            panel.Controls.Add(CreateFieldLabel("Email"), 0, 2);
            panel.Controls.Add(txtLoginEmail, 0, 3);
            panel.Controls.Add(new Label(), 0, 4);
            panel.Controls.Add(CreateFieldLabel("Password"), 0, 5);
            panel.Controls.Add(txtLoginPassword, 0, 6);
            panel.Controls.Add(btnLogin, 0, 7);
            panel.Controls.Add(lblLoginStatus, 0, 8);

            outer.Controls.Add(panel);
            return outer;
        }

        private void LoginClick(object sender, EventArgs e)
        {
            UserAccount user = inventoryService.Authenticate(txtLoginEmail.Text, txtLoginPassword.Text);
            if (user == null)
            {
                lblLoginStatus.ForeColor = Danger;
                lblLoginStatus.Text = "Invalid email or password.";
                txtLoginPassword.SelectAll();
                txtLoginPassword.Focus();
                return;
            }

            currentUser = user;
            BuildDashboardView();
        }

        private void BuildDashboardView()
        {
            SuspendLayout();
            Controls.Clear();
            AcceptButton = null;

            TableLayoutPanel shell = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = AppBackground,
                RowCount = 3,
                ColumnCount = 1
            };
            shell.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            shell.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            shell.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            shell.Controls.Add(CreateHeader(), 0, 0);
            shell.Controls.Add(CreateDashboardContent(), 0, 1);
            shell.Controls.Add(CreateStatusBar(), 0, 2);

            Controls.Add(shell);
            ResumeLayout(true);

            ClearEditor();
            RefreshProducts(null);
            SetStatus("Ready.");
        }

        private Control CreateHeader()
        {
            TableLayoutPanel header = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = HeaderBackground,
                Padding = new Padding(22, 10, 22, 10),
                ColumnCount = 4,
                RowCount = 1
            };
            header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));

            Label title = new Label
            {
                Dock = DockStyle.Fill,
                Text = "StockFlow Inventory Management",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label user = new Label
            {
                Dock = DockStyle.Fill,
                Text = currentUser == null ? string.Empty : currentUser.Name + " (" + currentUser.UserLevel + ")",
                ForeColor = Color.FromArgb(226, 232, 240),
                TextAlign = ContentAlignment.MiddleRight
            };

            Button reset = CreateButton("Reset Demo", Color.FromArgb(71, 85, 105));
            reset.Click += ResetDemoClick;

            Button logout = CreateButton("Logout", Danger);
            logout.Click += delegate
            {
                currentUser = null;
                BuildLoginView();
            };

            header.Controls.Add(title, 0, 0);
            header.Controls.Add(user, 1, 0);
            header.Controls.Add(reset, 2, 0);
            header.Controls.Add(logout, 3, 0);

            return header;
        }

        private Control CreateDashboardContent()
        {
            SplitContainer split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                BackColor = AppBackground,
                FixedPanel = FixedPanel.Panel2,
                SplitterDistance = 850,
                SplitterWidth = 6,
                Panel1MinSize = 650,
                Panel2MinSize = 360
            };

            split.Panel1.Controls.Add(CreateInventoryPanel());
            split.Panel2.Controls.Add(CreateEditorPanel());

            return split;
        }

        private Control CreateInventoryPanel()
        {
            TableLayoutPanel inventory = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = AppBackground,
                Padding = new Padding(18),
                RowCount = 3,
                ColumnCount = 1
            };
            inventory.RowStyles.Add(new RowStyle(SizeType.Absolute, 112F));
            inventory.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            inventory.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            inventory.Controls.Add(CreateStatsPanel(), 0, 0);
            inventory.Controls.Add(CreateFilterPanel(), 0, 1);
            inventory.Controls.Add(CreateProductGrid(), 0, 2);

            return inventory;
        }

        private Control CreateStatsPanel()
        {
            TableLayoutPanel stats = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = AppBackground,
                ColumnCount = 4,
                RowCount = 1
            };

            for (int i = 0; i < 4; i++)
            {
                stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }

            stats.Controls.Add(CreateStatCard("Products", out lblTotalProducts), 0, 0);
            stats.Controls.Add(CreateStatCard("Units in stock", out lblTotalStock), 1, 0);
            stats.Controls.Add(CreateStatCard("Low or out", out lblLowStock), 2, 0);
            stats.Controls.Add(CreateStatCard("Inventory value", out lblInventoryValue), 3, 0);

            return stats;
        }

        private Control CreateStatCard(string caption, out Label valueLabel)
        {
            Panel card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = PanelBackground,
                Margin = new Padding(0, 0, 12, 12),
                Padding = new Padding(18)
            };

            TableLayoutPanel stack = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            stack.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            stack.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));

            valueLabel = new Label
            {
                Dock = DockStyle.Fill,
                Text = "0",
                ForeColor = TextPrimary,
                Font = new Font("Segoe UI", 22F, FontStyle.Bold, GraphicsUnit.Point),
                TextAlign = ContentAlignment.BottomLeft
            };

            Label captionLabel = new Label
            {
                Dock = DockStyle.Fill,
                Text = caption,
                ForeColor = TextSecondary,
                TextAlign = ContentAlignment.TopLeft
            };

            stack.Controls.Add(valueLabel, 0, 0);
            stack.Controls.Add(captionLabel, 0, 1);
            card.Controls.Add(stack);

            return card;
        }

        private Control CreateFilterPanel()
        {
            TableLayoutPanel filters = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = PanelBackground,
                Padding = new Padding(12, 8, 12, 8),
                ColumnCount = 6,
                RowCount = 1
            };
            filters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 78F));
            filters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            filters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
            filters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
            filters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            filters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 116F));

            txtSearch = CreateTextBox(string.Empty);
            txtSearch.PlaceholderText = "Search products, SKU, supplier";
            txtSearch.TextChanged += delegate { RefreshProducts(selectedProductId); };

            cmbCategoryFilter = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCategoryFilter.Items.Add("All categories");
            foreach (ProductCategory category in Enum.GetValues(typeof(ProductCategory)))
            {
                cmbCategoryFilter.Items.Add(category);
            }
            cmbCategoryFilter.SelectedIndex = 0;
            cmbCategoryFilter.SelectedIndexChanged += delegate { RefreshProducts(selectedProductId); };

            Button clear = CreateButton("Clear", Color.FromArgb(71, 85, 105));
            clear.Click += delegate
            {
                txtSearch.Clear();
                cmbCategoryFilter.SelectedIndex = 0;
            };

            Button export = CreateButton("Export CSV", Success);
            export.Click += ExportClick;

            filters.Controls.Add(CreateInlineLabel("Search"), 0, 0);
            filters.Controls.Add(txtSearch, 1, 0);
            filters.Controls.Add(CreateInlineLabel("Category"), 2, 0);
            filters.Controls.Add(cmbCategoryFilter, 3, 0);
            filters.Controls.Add(clear, 4, 0);
            filters.Controls.Add(export, 5, 0);

            return filters;
        }

        private Control CreateProductGrid()
        {
            gridProducts = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = PanelBackground,
                BorderStyle = BorderStyle.None,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                DataSource = productBindingSource,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                EnableHeadersVisualStyles = false
            };

            gridProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(226, 232, 240);
            gridProducts.ColumnHeadersDefaultCellStyle.ForeColor = TextPrimary;
            gridProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            gridProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            gridProducts.DefaultCellStyle.SelectionForeColor = TextPrimary;
            gridProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            gridProducts.Columns.Add(CreateColumn("Sku", "SKU", 80));
            gridProducts.Columns.Add(CreateColumn("ProductName", "Product", 155));
            gridProducts.Columns.Add(CreateColumn("ProductCategory", "Category", 105));
            gridProducts.Columns.Add(CreateColumn("Supplier", "Supplier", 125));
            gridProducts.Columns.Add(CreateNumberColumn("FinalPrice", "Price", 80, "N2"));
            gridProducts.Columns.Add(CreateNumberColumn("Stock", "Stock", 60, "N0"));
            gridProducts.Columns.Add(CreateNumberColumn("LowStock", "Low", 52, "N0"));
            gridProducts.Columns.Add(CreateColumn("StockInformation", "Status", 92));
            gridProducts.Columns.Add(CreateNumberColumn("InventoryValue", "Value", 95, "N2"));

            gridProducts.SelectionChanged += ProductSelectionChanged;
            gridProducts.RowPrePaint += ProductGridRowPrePaint;

            return gridProducts;
        }

        private Control CreateEditorPanel()
        {
            Panel editor = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = PanelBackground,
                Padding = new Padding(18),
                AutoScroll = true
            };

            TableLayoutPanel form = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 2,
                RowCount = 0
            };
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            Label title = new Label
            {
                Dock = DockStyle.Top,
                Text = "Product Details",
                ForeColor = TextPrimary,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point),
                Height = 42
            };

            txtProductId = CreateTextBox(string.Empty);
            txtProductId.ReadOnly = true;
            txtSku = CreateTextBox(string.Empty);
            txtProductName = CreateTextBox(string.Empty);
            txtSupplier = CreateTextBox(string.Empty);

            cmbProductCategory = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = Enum.GetValues(typeof(ProductCategory))
            };

            numOriginalPrice = CreateNumeric(0, 100000000, 2);
            numOffer = CreateNumeric(0, 100, 2);
            numTax = CreateNumeric(0, 100, 2);
            numStock = CreateNumeric(0, 100000000, 0);
            numLowStock = CreateNumeric(0, 100000000, 0);

            AddEditorRow(form, "Product ID", txtProductId);
            AddEditorRow(form, "SKU", txtSku);
            AddEditorRow(form, "Name", txtProductName);
            AddEditorRow(form, "Category", cmbProductCategory);
            AddEditorRow(form, "Supplier", txtSupplier);
            AddEditorRow(form, "Price", numOriginalPrice);
            AddEditorRow(form, "Offer %", numOffer);
            AddEditorRow(form, "Tax %", numTax);
            AddEditorRow(form, "Stock", numStock);
            AddEditorRow(form, "Low stock", numLowStock);

            FlowLayoutPanel actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Height = 96,
                Padding = new Padding(0, 12, 0, 0)
            };

            Button addNew = CreateButton("New", Color.FromArgb(71, 85, 105));
            addNew.Width = 90;
            addNew.Click += delegate
            {
                ClearEditor();
                gridProducts.ClearSelection();
                txtProductName.Focus();
            };

            Button save = CreateButton("Save", PrimaryAction);
            save.Width = 90;
            save.Click += SaveProductClick;

            Button delete = CreateButton("Delete", Danger);
            delete.Width = 90;
            delete.Click += DeleteProductClick;

            Button stockIn = CreateButton("Stock +", Success);
            stockIn.Width = 90;
            stockIn.Click += delegate { AdjustStock(true); };

            Button stockOut = CreateButton("Stock -", Warning);
            stockOut.Width = 90;
            stockOut.Click += delegate { AdjustStock(false); };

            actions.Controls.Add(addNew);
            actions.Controls.Add(save);
            actions.Controls.Add(delete);
            actions.Controls.Add(stockIn);
            actions.Controls.Add(stockOut);

            lblEditorStatus = new Label
            {
                Dock = DockStyle.Top,
                Height = 64,
                ForeColor = TextSecondary,
                Text = "Create a new product or select one from the table.",
                TextAlign = ContentAlignment.TopLeft
            };

            editor.Controls.Add(lblEditorStatus);
            editor.Controls.Add(actions);
            editor.Controls.Add(form);
            editor.Controls.Add(title);

            return editor;
        }

        private Control CreateStatusBar()
        {
            TableLayoutPanel status = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(226, 232, 240),
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(12, 4, 12, 4)
            };
            status.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            status.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));

            lblStatus = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = TextPrimary,
                TextAlign = ContentAlignment.MiddleLeft
            };

            lblDataFile = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = TextSecondary,
                Text = "Data: " + inventoryService.DataFilePath,
                TextAlign = ContentAlignment.MiddleRight
            };

            status.Controls.Add(lblStatus, 0, 0);
            status.Controls.Add(lblDataFile, 1, 0);

            return status;
        }

        private void ProductSelectionChanged(object sender, EventArgs e)
        {
            if (gridProducts == null || gridProducts.CurrentRow == null)
            {
                return;
            }

            Product product = gridProducts.CurrentRow.DataBoundItem as Product;
            if (product == null)
            {
                return;
            }

            LoadProductIntoEditor(product.ProductId);
        }

        private void ProductGridRowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            Product product = gridProducts.Rows[e.RowIndex].DataBoundItem as Product;
            if (product == null)
            {
                return;
            }

            if (product.StockInformation == StockInformation.OutOfStock)
            {
                gridProducts.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(254, 226, 226);
            }
            else if (product.StockInformation == StockInformation.LowStock)
            {
                gridProducts.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(254, 243, 199);
            }
        }

        private void SaveProductClick(object sender, EventArgs e)
        {
            try
            {
                Product product = new Product
                {
                    ProductId = string.IsNullOrWhiteSpace(selectedProductId) ? Product.CreateProductId() : selectedProductId,
                    Sku = txtSku.Text,
                    ProductName = txtProductName.Text,
                    ProductCategory = (ProductCategory)cmbProductCategory.SelectedItem,
                    Supplier = txtSupplier.Text,
                    OriginalPrice = numOriginalPrice.Value,
                    OfferPercentage = numOffer.Value,
                    Tax = numTax.Value,
                    Stock = Convert.ToInt32(numStock.Value),
                    LowStock = Convert.ToInt32(numLowStock.Value)
                };

                Product saved = inventoryService.SaveProduct(product);
                selectedProductId = saved.ProductId;
                RefreshProducts(saved.ProductId);
                LoadProductIntoEditor(saved.ProductId);
                SetStatus("Saved " + saved.ProductName + ".");
                SetEditorStatus("Product saved.", false);
            }
            catch (Exception ex)
            {
                ShowEditorError(ex.Message);
            }
        }

        private void DeleteProductClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedProductId))
            {
                ShowEditorError("Select a product before deleting.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Delete this product from inventory?",
                "Confirm delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                return;
            }

            inventoryService.DeleteProduct(selectedProductId);
            string deletedId = selectedProductId;
            selectedProductId = null;
            RefreshProducts(null);
            SetStatus("Deleted product " + deletedId + ".");
        }

        private void AdjustStock(bool increase)
        {
            if (string.IsNullOrWhiteSpace(selectedProductId))
            {
                ShowEditorError("Select a product before adjusting stock.");
                return;
            }

            using (QuantityDialog dialog = new QuantityDialog(increase ? "Stock In" : "Stock Out"))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    int change = increase ? dialog.Quantity : -dialog.Quantity;
                    Product updated = inventoryService.AdjustStock(selectedProductId, change);
                    RefreshProducts(updated.ProductId);
                    LoadProductIntoEditor(updated.ProductId);
                    SetStatus((increase ? "Added " : "Removed ") + dialog.Quantity + " units for " + updated.ProductName + ".");
                }
                catch (Exception ex)
                {
                    ShowEditorError(ex.Message);
                }
            }
        }

        private void ExportClick(object sender, EventArgs e)
        {
            IReadOnlyList<Product> products = GetFilteredProducts();
            if (!products.Any())
            {
                SetStatus("Nothing to export.", true);
                return;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                dialog.FileName = "inventory-products-" + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + ".csv";

                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                inventoryService.ExportProductsCsv(dialog.FileName, products);
                SetStatus("Exported " + products.Count + " products to " + dialog.FileName + ".");
            }
        }

        private void ResetDemoClick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Reset all local data to the sample inventory and default admin account?",
                "Reset demo data",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                return;
            }

            inventoryService.ResetDemoData();
            selectedProductId = null;
            RefreshProducts(null);
            SetStatus("Demo data reset.");
        }

        private void RefreshProducts(string productIdToSelect)
        {
            if (productBindingSource == null)
            {
                return;
            }

            IReadOnlyList<Product> products = GetFilteredProducts();
            productBindingSource.DataSource = new BindingList<Product>(products.ToList());

            UpdateStats();

            if (!string.IsNullOrWhiteSpace(productIdToSelect))
            {
                SelectProductInGrid(productIdToSelect);
            }
            else if (gridProducts != null && gridProducts.Rows.Count > 0)
            {
                gridProducts.Rows[0].Selected = true;
                gridProducts.CurrentCell = gridProducts.Rows[0].Cells[0];
            }
            else
            {
                ClearEditor();
            }
        }

        private IReadOnlyList<Product> GetFilteredProducts()
        {
            ProductCategory? category = null;
            if (cmbCategoryFilter != null && cmbCategoryFilter.SelectedItem is ProductCategory)
            {
                category = (ProductCategory)cmbCategoryFilter.SelectedItem;
            }

            string search = txtSearch == null ? string.Empty : txtSearch.Text;
            return inventoryService.SearchProducts(search, category);
        }

        private void UpdateStats()
        {
            IReadOnlyList<Product> allProducts = inventoryService.Products;

            if (lblTotalProducts != null)
            {
                lblTotalProducts.Text = allProducts.Count.ToString("N0", CultureInfo.CurrentCulture);
                lblTotalStock.Text = allProducts.Sum(product => product.Stock).ToString("N0", CultureInfo.CurrentCulture);
                lblLowStock.Text = allProducts.Count(product => product.StockInformation != StockInformation.InStock).ToString("N0", CultureInfo.CurrentCulture);
                lblInventoryValue.Text = allProducts.Sum(product => product.InventoryValue).ToString("N2", CultureInfo.CurrentCulture);
            }
        }

        private void SelectProductInGrid(string productId)
        {
            if (gridProducts == null)
            {
                return;
            }

            foreach (DataGridViewRow row in gridProducts.Rows)
            {
                Product product = row.DataBoundItem as Product;
                if (product != null && product.ProductId == productId)
                {
                    row.Selected = true;
                    gridProducts.CurrentCell = row.Cells[0];
                    return;
                }
            }
        }

        private void LoadProductIntoEditor(string productId)
        {
            Product product = inventoryService.GetProduct(productId);
            if (product == null)
            {
                ClearEditor();
                return;
            }

            selectedProductId = product.ProductId;
            txtProductId.Text = product.ProductId;
            txtSku.Text = product.Sku;
            txtProductName.Text = product.ProductName;
            cmbProductCategory.SelectedItem = product.ProductCategory;
            txtSupplier.Text = product.Supplier;
            numOriginalPrice.Value = Clamp(product.OriginalPrice, numOriginalPrice.Minimum, numOriginalPrice.Maximum);
            numOffer.Value = Clamp(product.OfferPercentage, numOffer.Minimum, numOffer.Maximum);
            numTax.Value = Clamp(product.Tax, numTax.Minimum, numTax.Maximum);
            numStock.Value = Clamp(product.Stock, numStock.Minimum, numStock.Maximum);
            numLowStock.Value = Clamp(product.LowStock, numLowStock.Minimum, numLowStock.Maximum);

            SetEditorStatus(product.StockInformation + " | Final price: " +
                            product.FinalPrice.ToString("N2", CultureInfo.CurrentCulture), false);
        }

        private void ClearEditor()
        {
            selectedProductId = null;

            if (txtProductId == null)
            {
                return;
            }

            txtProductId.Text = "(new)";
            txtSku.Clear();
            txtProductName.Clear();
            txtSupplier.Clear();
            cmbProductCategory.SelectedIndex = 0;
            numOriginalPrice.Value = 0;
            numOffer.Value = 0;
            numTax.Value = 0;
            numStock.Value = 0;
            numLowStock.Value = 5;
            SetEditorStatus("Create a new product or select one from the table.", false);
        }

        private void SetStatus(string message, bool isError = false)
        {
            if (lblStatus == null)
            {
                return;
            }

            lblStatus.ForeColor = isError ? Danger : TextPrimary;
            lblStatus.Text = message;
        }

        private void SetEditorStatus(string message, bool isError)
        {
            if (lblEditorStatus == null)
            {
                return;
            }

            lblEditorStatus.ForeColor = isError ? Danger : TextSecondary;
            lblEditorStatus.Text = message;
        }

        private void ShowEditorError(string message)
        {
            SetEditorStatus(message, true);
            SetStatus(message, true);
        }

        private static TextBox CreateTextBox(string text)
        {
            return new TextBox
            {
                Dock = DockStyle.Fill,
                Text = text,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                Margin = new Padding(0, 3, 8, 3)
            };
        }

        private static Label CreateFieldLabel(string text)
        {
            return new Label
            {
                Dock = DockStyle.Fill,
                Text = text,
                ForeColor = TextSecondary,
                TextAlign = ContentAlignment.BottomLeft
            };
        }

        private static Label CreateInlineLabel(string text)
        {
            return new Label
            {
                Dock = DockStyle.Fill,
                Text = text,
                ForeColor = TextSecondary,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private static Button CreateButton(string text, Color color)
        {
            Button button = new Button
            {
                Dock = DockStyle.Fill,
                Text = text,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point),
                Margin = new Padding(4)
            };
            button.FlatAppearance.BorderSize = 0;
            return button;
        }

        private static NumericUpDown CreateNumeric(decimal minimum, decimal maximum, int decimalPlaces)
        {
            return new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = minimum,
                Maximum = maximum,
                DecimalPlaces = decimalPlaces,
                ThousandsSeparator = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                Margin = new Padding(0, 3, 8, 3)
            };
        }

        private static DataGridViewTextBoxColumn CreateColumn(string propertyName, string header, float fillWeight)
        {
            return new DataGridViewTextBoxColumn
            {
                DataPropertyName = propertyName,
                HeaderText = header,
                FillWeight = fillWeight,
                MinimumWidth = 60
            };
        }

        private static DataGridViewTextBoxColumn CreateNumberColumn(string propertyName, string header, float fillWeight, string format)
        {
            DataGridViewTextBoxColumn column = CreateColumn(propertyName, header, fillWeight);
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Format = format;
            return column;
        }

        private static void AddEditorRow(TableLayoutPanel table, string labelText, Control control)
        {
            int row = table.RowCount++;
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));

            Label label = CreateInlineLabel(labelText);
            label.Margin = new Padding(0, 3, 8, 3);

            table.Controls.Add(label, 0, row);
            table.Controls.Add(control, 1, row);
        }

        private static decimal Clamp(decimal value, decimal minimum, decimal maximum)
        {
            if (value < minimum)
            {
                return minimum;
            }

            if (value > maximum)
            {
                return maximum;
            }

            return value;
        }

        private static Image LoadImage(string fileName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", fileName);
            if (!File.Exists(path))
            {
                path = Path.Combine(Application.StartupPath, "Resources", fileName);
            }

            if (!File.Exists(path))
            {
                return null;
            }

            Image image = Image.FromFile(path);
            Bitmap copy = new Bitmap(image);
            image.Dispose();
            return copy;
        }

        private sealed class QuantityDialog : Form
        {
            private readonly NumericUpDown quantity;

            public QuantityDialog(string title)
            {
                Text = title;
                Width = 320;
                Height = 170;
                MinimizeBox = false;
                MaximizeBox = false;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;

                TableLayoutPanel layout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(16),
                    RowCount = 3,
                    ColumnCount = 2
                };
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

                quantity = CreateNumeric(1, 1000000, 0);
                quantity.Value = 1;

                Button ok = CreateButton("OK", PrimaryAction);
                ok.DialogResult = DialogResult.OK;

                Button cancel = CreateButton("Cancel", Color.FromArgb(71, 85, 105));
                cancel.DialogResult = DialogResult.Cancel;

                layout.Controls.Add(CreateInlineLabel("Quantity"), 0, 0);
                layout.Controls.Add(quantity, 1, 0);
                layout.Controls.Add(ok, 0, 1);
                layout.Controls.Add(cancel, 1, 1);

                Controls.Add(layout);
                AcceptButton = ok;
                CancelButton = cancel;
            }

            public int Quantity
            {
                get { return Convert.ToInt32(quantity.Value); }
            }
        }
    }
}
