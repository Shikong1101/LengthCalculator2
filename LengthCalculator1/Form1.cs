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
        private string currentInput = "";  // 儲存當前輸入的數字或文字
        private double result = 0;         // 儲存計算結果
        private string lastOperator = "";  // 儲存上一個運算符
        private bool isOperatorPressed = false; // 判斷是否按下了運算符

        public Form1()
        {
            InitializeComponent();  // 初始化視窗組件
        }

        // 處理數字按鍵的事件
        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;  // 將發送事件的按鈕轉換為Button物件

            // 如果按下了運算符，再按數字時清空當前輸入
            if (isOperatorPressed)
            {
                currentInput = button.Text; // 當前輸入設為剛按下的數字
                isOperatorPressed = false;  // 重置運算符按鈕已按下的標誌
            }
            else
            {
                currentInput += button.Text; // 在當前輸入後加上新按下的數字
            }

            textBoxDisplay.Text = currentInput; // 顯示當前輸入
        }

        // 處理運算符按鍵的事件（如 +, -, *, /）
        private void OperatorButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;  // 將發送事件的按鈕轉換為Button物件

            // 如果有輸入的數字，則執行當前計算
            if (currentInput != "")
            {
                // 如果之前已經有運算符，則進行計算並更新結果
                if (lastOperator != "")
                {
                    Calculate();
                }

                result = double.Parse(currentInput);  // 將當前輸入的數字存入結果
                currentInput = "";  // 清空當前輸入
            }

            lastOperator = button.Text;  // 保存當前按下的運算符
            isOperatorPressed = true;  // 設置運算符按鈕已按下的標誌
        }

        // 根據上一個運算符進行計算
        private void Calculate()
        {
            switch (lastOperator)
            {
                case "+":  // 加法運算
                    result += double.Parse(currentInput);
                    break;
                case "-":  // 減法運算
                    result -= double.Parse(currentInput);
                    break;
                case "*":  // 乘法運算
                    result *= double.Parse(currentInput);
                    break;
                case "/":  // 除法運算
                    if (double.Parse(currentInput) != 0)  // 防止除以零
                    {
                        result /= double.Parse(currentInput);
                    }
                    else
                    {
                        // 若除數為零，顯示錯誤訊息
                        MessageBox.Show("除數不能為零", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
            }

            textBoxDisplay.Text = result.ToString(); // 顯示計算結果
            currentInput = result.ToString();  // 更新當前輸入為計算結果
        }

        // 清除顯示區域並重置計算
        private void ClearButton_Click(object sender, EventArgs e)
        {
            currentInput = "";  // 清空當前輸入
            result = 0;  // 重置計算結果
            textBoxDisplay.Text = "0";  // 顯示初始值0
        }

        // 計算百分比的功能
        private void PercentButton_Click(object sender, EventArgs e)
        {
            if (currentInput != "")  // 確保當前輸入不為空
            {
                double value = double.Parse(currentInput);  // 轉換當前輸入為數字
                currentInput = (value / 100).ToString();  // 計算百分比
                textBoxDisplay.Text = currentInput;  // 顯示計算後的結果
            }
        }

        // 倒退按鍵的功能，刪除當前輸入的最後一個字元
        private void BackspaceButton_Click(object sender, EventArgs e)
        {
            if (currentInput.Length > 0)  // 確保當前輸入不為空
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);  // 刪除最後一個字元
                textBoxDisplay.Text = currentInput;  // 顯示更新後的結果
            }
        }

        // 等號按鈕的功能，計算並顯示最終結果
        private void EqualsButton_Click(object sender, EventArgs e)
        {
            if (currentInput != "")  // 確保當前輸入不為空
            {
                Calculate();  // 進行計算
                lastOperator = "";  // 清空運算符
            }
        }
    }
}

