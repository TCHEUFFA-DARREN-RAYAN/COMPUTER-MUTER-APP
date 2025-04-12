partial class Form2
{
    private System.ComponentModel.IContainer components = null;
    private DateTimePicker dateTimePicker;
    private Button btnSetDate;

    private void InitializeComponent()
    {
        this.dateTimePicker = new DateTimePicker();
        this.btnSetDate = new Button();

        // 
        // dateTimePicker
        // 
        this.dateTimePicker.Location = new System.Drawing.Point(30, 30);
        this.dateTimePicker.Name = "dateTimePicker";
        this.dateTimePicker.Size = new System.Drawing.Size(200, 30);

        // 
        // btnSetDate
        // 
        this.btnSetDate.Location = new System.Drawing.Point(30, 80);
        this.btnSetDate.Name = "btnSetDate";
        this.btnSetDate.Size = new System.Drawing.Size(200, 50);
        this.btnSetDate.Text = "Set Date";
        this.btnSetDate.Click += new EventHandler(this.btnSetDate_Click);

        // 
        // Form2
        // 
        this.ClientSize = new System.Drawing.Size(300, 150);
        this.Controls.Add(this.dateTimePicker);
        this.Controls.Add(this.btnSetDate);
        this.Name = "Form2";
        this.Text = "Select Specific Date";
    }
}
