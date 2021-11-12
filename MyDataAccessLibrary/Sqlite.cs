using System;
using System.Data;
using System.Windows;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace MyDataAccessLibrary;

public static class Sqlite
{
    public static string PathDB;
    public static DataTable Select(string sql)
    {

        try
        {
            using (SqliteConnection db = new SqliteConnection($"Filename={PathDB}"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand(sql, db);
                using (SqliteDataReader q = selectCommand.ExecuteReader())
                {
                    DataTable result = new DataTable();
                    result.Load(q);
                    return result;
                }
            }
        }
        catch (Exception e)
        {
            _ = MessageBox.Show("Ошибка открытия или обработки базы данных.\n\n" + e, "Ошибка!");
            throw new InvalidOperationException("Ошибка открытия или обработки базы данных", e);
        }
    }

    public static int ExecNonQuery(string sql)
    {
        int result = 0;
        try
        {
            using SqliteConnection db = new SqliteConnection($"Filename={PathDB}");
            db.Open();
            SqliteCommand execCommand;
            using (SqliteTransaction transaction = db.BeginTransaction())
            {
                execCommand = db.CreateCommand();
                execCommand.CommandText = sql;
                result = execCommand.ExecuteNonQuery();
                transaction.Commit();
            }
            db.Close();
        }
        catch (Exception e)
        {
            _ = MessageBox.Show("Ошибка открытия или обработки базы данных.\n\n" + e, "Ошибка!");
            throw new InvalidOperationException("Ошибка открытия или обработки базы данных", e);
        }
        return result;
    }
    public static int ExecNonQueryRange(List<string> listSQL)
    {
        int result = 0;
        try
        {
            using SqliteConnection db = new SqliteConnection($"Filename={PathDB}");
            db.Open();
            SqliteCommand execCommand;
            using (SqliteTransaction transaction = db.BeginTransaction())
            {
                execCommand = db.CreateCommand();
                foreach (string sql in listSQL)
                {
                    execCommand.CommandText = sql;
                    result += execCommand.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            db.Close();
        }
        catch (Exception e)
        {
            _ = MessageBox.Show("Ошибка открытия или обработки базы данных.\n\n" + e, "Ошибка!");
            throw new InvalidOperationException("Ошибка открытия или обработки базы данных", e);
        }
        return result;
    }

}
