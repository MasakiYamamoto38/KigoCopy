using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Linq;

public static class KigoSearch
{
    private const int COLUM_VALUE_LENGTH = 11;
    private static Kigo kigoMng = new Kigo();
    /// <summary>
    /// 格納されているデータを取得します
    /// </summary>
    public static Kigo kigoManager { get { return kigoMng; } }

    /// <summary>
    /// 検索ワードに対する結果を取得します
    /// </summary>
    /// <param name="word">頭文字もしくは任意の文字列</param>
    /// <returns></returns>
    public static (string college, string department, bool licenseTf, string kigo, Kigo.initialBox initial) []
        getResult(string word)
    {
        var inis = kigoMng.initials;
        var ls = new List<int>();
        for (int i = 0; i < inis.Count; i++)
        {
            var ini = (inis[i]).data;
            var strs = ini.Where(elm => elm.Contains(word));
            if (strs.Count() >= 1) ls.Add(i);
            /*for (int j = 0; j < ini.Length; j++)
            {
                if (ini[j] == word)
                { ls.Add(i); break; }
            }*/
            
        }

        var tps = 
            new(string college, string department, bool licenseTf, string kigo, Kigo.initialBox initial)[ls.Count];
        for (int i = 0; i < tps.Length; i++)
        {
            tps[i] = kigoMng.getRecod(idx: ls[i]);
        }
        return tps;
    }
        
    /// <summary>
    ///　CSVファイルを読み込み、データをアプリ内で使用できるようにします。
    ///　また、初期処理として必ず実行する必要があります。
    /// </summary>
    /// <param name="Path">CSVファイルのフルパスを指定します</param>
    public static void inuptKigoData(string Path)
    {
        try
        {
            var cReader =
            (new StreamReader(Path, System.Text.Encoding.UTF8));

            while (cReader.Peek() >= 0)
            {
                var line = cReader.ReadLine();
                var comlin = line.Split('#');
                if (comlin.Length >= 2) continue;

                var values = line.Split(',');
                if (values.Length != COLUM_VALUE_LENGTH) continue;

                var ksm = new List<string>();
                for (int i = 4; i <= 10; i++)
                {
                    if (values[i] != "" || values[i] != null)
                        ksm.Add(values[i]);
                }
                var strs = new string[ksm.Count];
                for (int i = 0; i < ksm.Count; i++)
                { strs[i] = ksm[i]; }

                bool bl = false;
                if (values[2] == "TRUE") bl = true;

                kigoMng.addRecord(
                    college: values[0], department: values[1], licenseTf: bl, kigo: values[3], initial: strs);
            }
            cReader.Close();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, caption: "CSVデータ読み込みに失敗しました");
        }
    }
}