using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
        }
        public List<Student> list = new List<Student>
            {
                new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },
                new Student{ Name = "ggg", Class = "CS_101", Chi = 93, Eng = 96, Math = 97, Gender = "Male" },
                new Student{ Name = "hhh", Class = "CS_102", Chi = 65, Eng = 68, Math =59, Gender = "Male" },
            };
        public class Student
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; }
            public int Eng { get; set; }
            public int Math { get; set; }
            public string Gender { get; set; }
            public int SumScore()
            { return Chi + Eng + Math; }
            public int AvgScore()
            { return (Chi + Eng + Math) / 3; }
            public (string subject, int score) MaxScore()
            {
                if (Chi >= Eng && Chi >= Math)
                    return ("Chi", Chi);
                else if (Eng >= Chi && Eng >= Math)
                    return ("Eng", Eng);
                else
                    return ("Math", Math);
            }
            public (string subject, int score) MinScore()
            {
                if (Chi <= Eng && Chi <= Math)
                    return ("Chi", Chi);
                else if (Eng <= Chi && Eng <= Math)
                    return ("Eng", Eng);
                else
                    return ("Math", Math);
            }
        }
        string Split(int n)
        {
            if (n <= 69)
            { return "Avg待加強"; }
            else if (n >= 70 && n <= 89)
            { return "Avg佳"; }
            else
            { return "Avg優良"; }
        }
        string Filesize(long l)
        {
            if ( l <= 16*1024)
            { return "小(0-16KB)"; }
            else if ( l<= 1024*1024)
            { return "中(16KB-1MB)"; }
            else
            { return "大(1MB以上)"; }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // 找出 後面兩個 的學員所有科目成績
            var lastTwo = list.Skip(list.Count - 2).Select(p => new { Name = p.Name, Chi = p.Chi, Eng = p.Eng, Math = p.Math });
            this.dataGridView1.DataSource = lastTwo.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            // 找出 前面三個 的學員所有科目成績
            var topThree = list.Take(3).Select(p => new { Name = p.Name, Chi = p.Chi, Eng = p.Eng, Math = p.Math });
            this.dataGridView1.DataSource = topThree.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績
            var s = from p in list
                    where p.Name == "aaa" || p.Name == "bbb" || p.Name == "ccc"
                    select (new { p.Name, Chi = p.Chi, Eng = p.Eng, p.Math });
            this.dataGridView1.DataSource = s.ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // 找出學員 'bbb' 的成績
            var b = list.Where(p => p.Name == "bbb").Select(p => new { p.Name, Chi = p.Chi, Eng = p.Eng, p.Math });
            this.dataGridView1.DataSource = b.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)
            var noB = list.Where(p => p.Name != "bbb").Select(p => new { p.Name, Chi = p.Chi, Eng = p.Eng, p.Math });
            this.dataGridView1.DataSource = noB.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績
            var studntScore = list.Where(p => p.Name == "aaa" || p.Name == "bbb" || p.Name == "ccc").Select(p => new { p.Name, Chi = p.Chi, p.Math });
            this.dataGridView1.DataSource = studntScore.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //數學不及格... 是誰
            var mathNo60 = list.Where(p => p.Math < 60).Select(p => new { p.Name, p.Math });
            this.dataGridView1.DataSource = mathNo60.ToList();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            // 個人 sum, min, max, avg
            // 統計 每個學生個人成績 並排序
            var list1 = list.OrderByDescending(student => student.SumScore()).ToList();
            listBox1.Items.Clear();
            foreach (var student in list1)
            {
                int SumScore = student.SumScore();
                int AvgScore = student.AvgScore();
                var MaxScore = student.MaxScore();
                var MinScore = student.MinScore();
                listBox1.Items.Add($"{student.Name}  Sum(排序)={SumScore}  Avg={AvgScore}  Max={MaxScore}  Min={MinScore}");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
            var q = from n in list
                    let avgScore = (n.Math + n.Chi + n.Eng) / 3
                    group n by Split(avgScore) into g
                    select new
                    {
                        g.Key,
                        Count = g.Count(),
                        MyGroup = g,
                        // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
                        Students = g.OrderByDescending(s => (s.Math + s.Chi + s.Eng) / 3).ToList()
                    };
            this.treeView1.Nodes.Clear();

            foreach (var group in q)
            {
                string s = $"{group.Key} ({group.Count})";
                TreeNode node = this.treeView1.Nodes.Add(s);

                foreach (var student in group.Students)
                {
                    node.Nodes.Add($"{student.Name} ({student.AvgScore()})");
                }
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"C:\Windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    group f by Filesize(f.Length) into h
                    select new { h.Key, Count = h.Count(), FileGroup = h };
            this.treeView1.Nodes.Clear();

            foreach (var fgroup in q)
            {
                string s = $"{fgroup.Key} ({fgroup.Count})";
                TreeNode node = this.treeView1.Nodes.Add(s);

                foreach (var file in fgroup.FileGroup)
                {
                    node.Nodes.Add($"{file.Name} ({file.Length})");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"C:\Windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    group f by f.LastWriteTime.Year into h
                    select new { h.Key, Count = h.Count(), YearGroup = h };
            this.treeView1.Nodes.Clear();

            foreach (var Ygroup in q)
            {
                string s = $"{Ygroup.Key} ({Ygroup.Count})";
                TreeNode node = this.treeView1.Nodes.Add(s);

                foreach (var year in Ygroup.YearGroup)
                {
                    node.Nodes.Add($"{year.Name} ({year.LastWriteTime})");
                }
            }
        }
    }
}
