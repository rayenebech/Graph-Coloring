using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;

namespace Graph_Coloring
{

    public partial class Form1 : Form
    {
        public int[,] MatrixGenerator(List<List<string>> students)
        {
            int[,] graph = new int[students.Count(), students.Count()];
            int i;
            bool connected = false;
            for (int j = 0; j < students.Count(); j++)
            {
                for (int k = j + 1; k < students.Count(); k++)
                {
                    if (j == k)
                    {
                        graph[j, k] = 0;
                    }
                    else
                    {
                       i = 1;
                        connected = false;
                        while (!connected && i < students[j].Count())
                        {
                            if (students[k].Contains(students[j][i]))
                            {
                                connected = true;
                                graph[j, k] = 1;
                                graph[k, j] = 1;
                            }
                            i++;
                        }
                    }

                }
            }
            return graph;

        }
        public List<List<string>> ReadFiles()
        {
            int i = 0;
            List<List<string>> students = new List<List<string>>();
            StreamReader dersler = new StreamReader("../../Veriler/DersListesi.txt");
            string ders = dersler.ReadLine();
            while (ders != null)
            {
                students.Add(new List<String>());
                students[i].Add(ders);
                StreamReader ogrenciler = new StreamReader("../../Veriler/" + ders + ".txt");
                string ogrenci = ogrenciler.ReadLine();
                while (ogrenci != null)
                {
                    students[i].Add(ogrenci);
                    ogrenci = ogrenciler.ReadLine();
                }
                ders = dersler.ReadLine();
                i++;
            }
            return students;
        }
        public static void PrintMatrix(int[,] matrix,int n, int m)
        {
            for (int i = 0; i <n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }


        }
        
        public List<int> ColourGraph(int[,] graph, int size, ref int min)
        {
            List<int> colourList = new List<int>();
            //This is a list of the colours used for each node. The index-> Vertex and the value -> The colour number
            int colour=0,j;
            bool safe, notColoured;
            for(int i=0;i<size; i++)
            {         
                notColoured = true;
                colour = 0;
                while (notColoured)
                {
                    safe = true;
                    j = 0;
                    while (j < i && safe)
                    {
                        if (graph[i, j] == 1 && colour == colourList[j])
                        {
                            safe = false;
                        }
                        j++;
                    }
                    if (safe)
                    {
                        colourList.Add(colour);
                        notColoured = false;
                    }
                    else
                    {
                        colour++;
                    }
                } 
            }
            min = colour;
            return colourList;
        }

        public Form1()
        {
            InitializeComponent();

        }
        private void button3_Click(object sender, EventArgs e)
        {
        }

       
        //globaly define conection
        //SqlConnection con = new SqlConnection("data source=DESKTOP-F3B3CCJ\SQLEXPRESS; database=master; uid=sa; password=123456;");

        public void Form1_Load(object sender, EventArgs e)
        {

            List<List<string>> students = ReadFiles();
            List<int> colours = new List<int>();
            int min = 0;
            int[,] AdjacencyMatrix = MatrixGenerator(students);
            int size = students.Count();
            PrintMatrix(AdjacencyMatrix, size, size);
            colours= ColourGraph(AdjacencyMatrix, size, ref min);
            Console.WriteLine("The min number of colours: " + min );    
            Console.WriteLine("Printing the colours");
            /*foreach (int i in colours)
            {
                Console.WriteLine(students[i] +"->"+ i);
            }*/



        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black);
            g.DrawEllipse(myPen, 500, 300, 50, 50);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            /*Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black);
            g.DrawEllipse(myPen, 200, 300, 50, 50);*/
        }

    }
}
