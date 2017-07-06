using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections;


public class Kigo
{
    public enum colum
    {
        college,
        department,
        licenseTf,
        kigo,
        initial
    }
    public List<string> colleges { get; set; }
    public List<string> departments { get; set; }
    public List<bool> licenseTfs { get; set; }
    public List<string> kigos { get; set; }
    public List<initialBox> initials { get; set; }

    public struct initialBox
    {
        public string[] data { get; set; }
    }

    public Kigo()
    {
        colleges = new List<string>();
        departments = new List<string>();
        licenseTfs = new List<bool>();
        kigos = new List<string>();
        initials = new List<initialBox>();
    }

    public (string college, string department, bool licenseTf, string kigo, initialBox initial) getRecod(int idx)
    {
        var tp = (college: colleges[idx], department: departments[idx],
            licenseTf: licenseTfs[idx], kigo: kigos[idx], initial: initials[idx]);
        return tp;
    }

    public void addRecord(
        string college, string department, bool licenseTf, string kigo, string[] initial)
    {
        colleges.Add(college);
        departments.Add(department);
        licenseTfs.Add(licenseTf);
        kigos.Add(kigo);
        var ib = new initialBox();
        ib.data = initial;
        initials.Add(ib);
    }
}