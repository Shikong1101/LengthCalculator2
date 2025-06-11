using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LengthCalculator1
{
    public partial class Form1 : Form
    {
        private string currentInput = "";  // 儲存當前輸入
        private double result = 0;         // 儲存計算結果
        private string lastOperator = "";  // 儲存上一個運算符
        private bool isOperatorPressed = false; // 判斷是否按下了運算符

        public Form1()
        {
            InitializeComponent();
        }

        // 處理數字按鍵
        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (isOperatorPressed)
            {
                currentInput = button.Text; // 當按下運算符後，再按數字時，清空當前輸入
                isOperatorPressed = false;
            }
            else
            {
                currentInput += button.Text;
            }

            textBoxDisplay.Text = currentInput; // 顯示當前輸入
        }

        // 處理運算符按鍵
        private void OperatorButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (currentInput != "")
            {
                if (lastOperator != "")
                {
                    Calculate(); // 當前計算，並更新結果
                }

                result = double.Parse(currentInput);
                currentInput = ""; // 清空當前輸入
            }

            lastOperator = button.Text;
            isOperatorPressed = true;
        }

        // 計算結果
        private void Calculate()
        {
            switch (lastOperator)
            {
                case "+":
                    result += double.Parse(currentInput);
                    break;
                case "-":
                    result -= double.Parse(currentInput);
                    break;
                case "*":
                    result *= double.Parse(currentInput);
                    break;
                case "/":
                    if (double.Parse(currentInput) != 0)
                    {
                        result /= double.Parse(currentInput);
                    }
                    else
                    {
                        MessageBox.Show("除數不能為零", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
            }

            textBoxDisplay.Text = result.ToString(); // 顯示計算結果
            currentInput = result.ToString();
        }

        // 清除顯示內容
        private void ClearButton_Click(object sender, EventArgs e)
        {
            currentInput = "";
            result = 0;
            textBoxDisplay.Text = "0";
        }

        // 計算百分比
        private void PercentButton_Click(object sender, EventArgs e)
        {
            if (currentInput != "")
            {
                double value = double.Parse(currentInput);
                currentInput = (value / 100).ToString();
                textBoxDisplay.Text = currentInput;
            }
        }

        // 倒退按鍵
        private void BackspaceButton_Click(object sender, EventArgs e)
        {
            if (currentInput.Length > 0)
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                textBoxDisplay.Text = currentInput;
            }
        }

        // 等號按鈕
        private void EqualsButton_Click(object sender, EventArgs e)
        {
            if (currentInput != "")
            {
                Calculate();
                lastOperator = ""; // 清空操作符
            }
        }
    }
}
