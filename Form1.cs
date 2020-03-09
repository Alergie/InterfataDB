using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace reteaTelefonie
{
    
    public class Form1 : Form
    {
        private string conString = "datasource=127.0.0.1;port=3306;username=root;password=;database=retea_telefonie;";
        private System.ComponentModel.IContainer components = null;
        private ListView listView1;
        private TextBox textBox1;
        private Button button3;
        private ComboBox comboBox1;
        private Label label2;
        private Button Stergere;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Label label4;
        private ComboBox comboBox2;
        private DataGridView dataGridView3;
        private Button button1;
        private Button button4;
        private Label label1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        public Form1()
        {
            this.InitializeComponent();
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            List<Tuple<string, string>> tables = GetTables(this.conString);
            int num = 0;
            while (true)
            {
                if (num >= tables.Count<Tuple<string, string>>())
                {
                    this.UpdateList();
                    return;
                }
                this.comboBox1.Items.Add(tables[num].Item1);
                num++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dataSource = (DataTable)this.dataGridView3.DataSource;
            string[] textArray1 = new string[] { "select * from ", this.comboBox1.Text, " where id_", this.comboBox1.Text, "=", this.comboBox2.Text };
            MySqlDataAdapter adapter = new MySqlDataAdapter(new MySqlCommand(string.Concat(textArray1), new MySqlConnection(this.conString)));
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            try
            {
                adapter.Fill(dataSource);
            }
            catch
            {
                MessageBox.Show("Select row");
            }
            try
            {
                adapter.Update(dataSource);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                this.dataGridView3.DataSource = dataTable;
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message);
            }
            this.UpdateList();
            this.updateDataGridView2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string text = this.textBox1.Text;
            MySqlConnection connection = new MySqlConnection(this.conString);
            connection.Open();
            try
            {
                DataTable dataTable = new DataTable();
                new MySqlDataAdapter(text, connection).Fill(dataTable);
                this.dataGridView1.DataSource = dataTable;
                connection.Close();
                this.UpdateList();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(this.conString);
            string[] textArray1 = new string[] { "delete from ", this.comboBox1.Text, " where id_", this.comboBox1.Text, "=", this.comboBox2.Text };
            MySqlCommand command = new MySqlCommand(string.Concat(textArray1), connection);
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
            MessageBox.Show("Deleted successfully!");
            this.UpdateList();
            this.updateDataGridView2();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.updateDataGridView2();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] textArray1 = new string[] { "select * from ", this.comboBox1.Text, " limit ", (int.Parse(this.comboBox2.Text) - 1).ToString(), ",1" };
            DataTable dataTable = new DataTable();
            new MySqlDataAdapter(string.Concat(textArray1), new MySqlConnection(this.conString)).Fill(dataTable);
            this.dataGridView3.DataSource = dataTable;
        }



        public static List<Tuple<string, string>> GetTables(string connectionString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                List<Tuple<string, string>> list = new List<Tuple<string, string>>();
                foreach (DataRow row in connection.GetSchema("Tables").Rows)
                {
                    MySqlCommand command = new MySqlCommand("select count(*) from " + row[2].ToString(), connection);
                    list.Add(new Tuple<string, string>(row[2].ToString(), command.ExecuteScalar().ToString()));
                }
                return list;
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Form1));
            this.listView1 = new ListView();
            this.textBox1 = new TextBox();
            this.button3 = new Button();
            this.comboBox1 = new ComboBox();
            this.label2 = new Label();
            this.Stergere = new Button();
            this.dataGridView1 = new DataGridView();
            this.dataGridView2 = new DataGridView();
            this.label4 = new Label();
            this.comboBox2 = new ComboBox();
            this.dataGridView3 = new DataGridView();
            this.button1 = new Button();
            this.button4 = new Button();
            this.label1 = new Label();
            ((ISupportInitialize)this.dataGridView1).BeginInit();
            ((ISupportInitialize)this.dataGridView2).BeginInit();
            ((ISupportInitialize)this.dataGridView3).BeginInit();
            base.SuspendLayout();
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(9, 10);
            this.listView1.Margin = new Padding(2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0xc0, 0x111);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.textBox1.Font = new Font("Consolas", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox1.Location = new Point(0xda, 10);
            this.textBox1.Margin = new Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x1d1, 0x31);
            this.textBox1.TabIndex = 4;
            this.button3.Font = new Font("Consolas", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.button3.Location = new Point(0x2af, 9);
            this.button3.Margin = new Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x55, 0x31);
            this.button3.TabIndex = 5;
            this.button3.Text = "Run SQL Query";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new EventHandler(this.button3_Click);
            this.comboBox1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(0x6f, 0x125);
            this.comboBox1.Margin = new Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(150, 0x1c);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label2.Location = new Point(11, 0x128);
            this.label2.Margin = new Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x60, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Table name:";
            this.Stergere.Font = new Font("Consolas", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Stergere.Location = new Point(0x109, 0x125);
            this.Stergere.Margin = new Padding(2);
            this.Stergere.Name = "Stergere";
            this.Stergere.Size = new Size(0x9b, 0x1d);
            this.Stergere.TabIndex = 10;
            this.Stergere.Text = "Delete table";
            this.Stergere.UseVisualStyleBackColor = true;
            this.Stergere.Click += new EventHandler(this.Stergere_Click);
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new Point(0xda, 0x3e);
            this.dataGridView1.Margin = new Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 0x18;
            this.dataGridView1.Size = new Size(0x22a, 0xdd);
            this.dataGridView1.TabIndex = 12;
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = Color.White;
            this.dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new Point(9, 0x147);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new Size(0x2fb, 0xc7);
            this.dataGridView2.TabIndex = 13;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label4.Location = new Point(5, 0x218);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x2d, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Row:";
            this.comboBox2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new Point(0x43, 0x215);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(50, 0x1c);
            this.comboBox2.TabIndex = 15;
            this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dataGridView3.BackgroundColor = Color.White;
            this.dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new Point(0x7b, 0x214);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new Size(0x289, 0x3a);
            this.dataGridView3.TabIndex = 0x10;
            this.button1.Font = new Font("Consolas", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.button1.Location = new Point(0x2a6, 0x254);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x5e, 0x1c);
            this.button1.TabIndex = 0x11;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button4.Font = new Font("Consolas", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.button4.Location = new Point(0x1fb, 0x254);
            this.button4.Name = "button4";
            this.button4.Size = new Size(0xa5, 0x1c);
            this.button4.TabIndex = 0x12;
            this.button4.Text = "Delete record";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new EventHandler(this.button4_Click);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(5, 570);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x47, 20);
            this.label1.TabIndex = 0x13;
            this.label1.Text = "Add row:";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            base.ClientSize = new Size(0x30f, 0x279);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.button4);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.dataGridView3);
            base.Controls.Add(this.comboBox2);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.dataGridView2);
            base.Controls.Add(this.dataGridView1);
            base.Controls.Add(this.Stergere);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.listView1);
            this.ForeColor = SystemColors.ControlText;
            base.FormBorderStyle = FormBorderStyle.FixedSingle;

            base.Margin = new Padding(2);
            base.Name = "Form1";
            this.Text = "DB interface";
            ((ISupportInitialize)this.dataGridView1).EndInit();
            ((ISupportInitialize)this.dataGridView2).EndInit();
            ((ISupportInitialize)this.dataGridView3).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void Stergere_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(this.conString);
            MySqlCommand command = new MySqlCommand("DROP TABLE " + this.comboBox1.Text, connection);
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
            MessageBox.Show("Deleted successfully!");
            this.UpdateList();
        }

        private void updateDataGridView2()
        {
            DataTable dataTable = new DataTable();
            new MySqlDataAdapter("select * from " + this.comboBox1.Text, new MySqlConnection(this.conString)).Fill(dataTable);
            this.dataGridView2.DataSource = dataTable;
            this.comboBox2.Items.Clear();
            List<Tuple<string, string>> tables = GetTables(this.conString);
            int num = 0;
            while (true)
            {
                if (num < tables.Count)
                {
                    if (tables[num].Item1 != this.comboBox1.Text)
                    {
                        num++;
                        continue;
                    }
                    int num2 = 0;
                    while (true)
                    {
                        if (num2 >= int.Parse(tables[num].Item2))
                        {
                            break;
                        }
                        this.comboBox2.Items.Add(dataTable.Rows[num2][0]);
                        num2++;
                    }
                }
                this.dataGridView2.Refresh();
                this.dataGridView3.DataSource = null;
                return;
            }
        }

        public void UpdateList()
        {
            this.listView1.Clear();
            this.listView1.View = View.Details;
            this.listView1.Columns.Add("Table");
            this.listView1.Columns.Add("Number of entries");
            List<Tuple<string, string>> tables = GetTables(this.conString);
            int num = 0;
            while (true)
            {
                if (num >= tables.Count<Tuple<string, string>>())
                {
                    this.listView1.Columns[0].Width = -1;
                    this.listView1.Columns[1].Width = -2;
                    this.listView1.Columns[1].TextAlign = HorizontalAlignment.Center;
                    return;
                }
                this.listView1.Items.Add(tables[num].Item1).SubItems.Add(tables[num].Item2.ToString());
                num++;
            }
        }
    }
}