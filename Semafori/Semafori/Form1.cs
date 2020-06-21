using System;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semafori
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static int[] array = new int[100];
        public static int[] SortedArray = new int[100]; 

        Thread BubbleThread = new Thread (bubbleSort);
        Thread MaxSortThread = new Thread (MaxSort);
        Thread BubbleThreadWithoutLock = new Thread (bubbleSortWithoutLock);
        Thread MaxSortThreadWithoutLock = new Thread (MaxSortWithoutLock);

        static object locker = new object();

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            Random ran = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = ran.Next(1, 100);
                textBox1.Text += array[i] + " ";
            }
            button1.Enabled = false;
            button2.Enabled = true;
            checkBox1.Enabled = true;
        }
        public static void bubbleSort()
        {
            SortedArray = array;
            lock (locker)
            {
                for (int i = 0; i < SortedArray.Length; i++)
                {
                    for (int j = i + 1; j < SortedArray.Length; j++)
                    {
                        if (SortedArray[i] > SortedArray[j])
                        {
                            int t = SortedArray[i];
                            SortedArray[i] = SortedArray[j];
                            SortedArray[j] = t;
                        }
                    }
                }
            }

        }
        public static void bubbleSortWithoutLock()
        {
            SortedArray = array;
            for (int i = 0; i < SortedArray.Length; i++)
            {
                for (int j = i + 1; j < SortedArray.Length; j++)
                {
                    if (SortedArray[i] > SortedArray[j])
                    {
                        int t = SortedArray[i];
                        SortedArray[i] = SortedArray[j];
                        SortedArray[j] = t;
                    }
                }
            }
        }
        public static void MaxSort()
        {
            lock (locker)
            {
                SortedArray = array;
                for (int i = 0; i < SortedArray.Length - 1; i++)
                {
                    int min = i;
                    for (int j = i + 1; j < SortedArray.Length; j++)
                    {
                        if (SortedArray[j] < SortedArray[min])
                        {
                            min = j;
                        }
                    }
                    int temp = SortedArray[min];
                    SortedArray[min] = SortedArray[i];
                    SortedArray[i] = temp;
                }
            }
        }
        public static void MaxSortWithoutLock()
        {
            SortedArray = array;
            for (int i = 0; i < SortedArray.Length - 1; i++)
            {
                int min = i;
                Thread.Sleep(1000);
                for (int j = i + 1; j < SortedArray.Length; j++)
                {
                    if (SortedArray[j] < SortedArray[min])
                    {
                        min = j;
                    }
                }
                int temp = SortedArray[min];
                SortedArray[min] = SortedArray[i];
                SortedArray[i] = temp;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            if (checkBox1.Checked == true)
            {
                BubbleThreadWithoutLock.Start();
                MaxSortThreadWithoutLock.Start();
            }
            else
            {
                BubbleThread.Start();
                MaxSortThread.Start();
            }
            for (int i = 0; i < SortedArray.Length; i++)
                textBox2.Text += SortedArray[i] + " ";
            button2.Enabled = false;
            checkBox1.Enabled = false;
            button3.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button1.Enabled = true;
            textBox1.Text = "Здесь будет выводиться сгенерированный массив";
            textBox2.Text = "Здесь будет выводиться результат сортировки массива";
        }
    }
}
