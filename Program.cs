using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ExcCRUD_C
{
    class Program
    {
        static void Main(string[] args)
        {
            //koneksi ke database
            SqlConnection con = new SqlConnection("Data Source = MSI; database = Team8PABD1; integrated security = True; MultipleActiveResultSets=true");
            //open koneksi
            con.Open();

            Console.WriteLine("Database connected");
            Console.WriteLine("Please click any key goto Menu");
            Console.ReadKey();
            Console.Clear();

            //perulangan menu pilihan
            char ch;
            do
            {
                a:
                Console.WriteLine("\n============================================");
                Console.WriteLine("EXERCISE 1 - PENGEMBANGAN APLIKASI BASISDATA");
                Console.WriteLine("============================================");

                Console.WriteLine("\nISI DATABASE :");
                string data = "SELECT * FROM DetailBarang";
                //membuat command untuk mengeksekusi sql query
                SqlCommand cmData = new SqlCommand(data, con);
                //membuat Dr data reader untuk mengeksekusi hasil apa pun yang ditetapkan dengan beberapa baris / kolom 
                SqlDataReader Dr = cmData.ExecuteReader();
                Console.WriteLine();

                while (Dr.Read())
                {
                    Console.Write("{0} - {1}\n", Dr["kd_faktur"], Dr["nama_barang"]);
                }

                Console.WriteLine();
                Console.WriteLine("Menu Pilihan");
                Console.WriteLine("1. Insert Barang");
                Console.WriteLine("2. Menampilkan Semua Barang");
                Console.WriteLine("3. Update Barang");
                Console.WriteLine("4. Delete Barang");
                Console.WriteLine("5. Search Barang");
                Console.WriteLine("6. Exit");
                Console.Write("Pilihan : ");
                //menyimpan pilihan
                string pilihan = Console.ReadLine();

                switch (pilihan)
                {
                    //1. Insert Barang
                    case "1":
                        b:
                        Console.WriteLine("\n~INSERT BARANG~");
                        Console.Write("Kode Faktur : ");
                        string Kd_faktur = Console.ReadLine();
                        
                        Console.Write("Nama Barang : ");
                        string Nama_barang = Console.ReadLine();
                        Console.Write("Harga : ");
                        string Harga = Console.ReadLine();
                        Console.Write("Jumlah : ");
                        string Jumlah = Console.ReadLine();

                        //membuat variable untuk di insert kedalam table DetailBarang
                        string strQueryInsert = String.Format("INSERT INTO DetailBarang(kd_faktur, nama_barang, harga, jumlah) " +
                            "VALUES('{0}', '{1}', '{2}', '{3}')", Kd_faktur, Nama_barang, Harga, Jumlah);
                        //membuat command cmdIN untuk di execute
                        SqlCommand cmdIN = new SqlCommand(strQueryInsert, con);
                        //membuat command cm membuat kondisi 
                        SqlCommand cm = new SqlCommand("SELECT kd_faktur FROM DetailBarang WHERE kd_faktur = @kd_faktur", con);
                        //cm dibuat parameter dengan kd_faktur
                        cm.Parameters.AddWithValue("kd_faktur", Kd_faktur);

                        //membuat variable isCheck untuk di cek apakah kd_faktur sudah ada atau belum
                        //jika ischeck tidak null maka 
                        var isCheck = Convert.ToString(cm.ExecuteScalar()) != "";
                        if (isCheck)
                        {
                            Console.WriteLine("\nkode faktur telah tersedia...");
                            Console.WriteLine("\nPress any key to insert again...");
                            Console.ReadKey();
                            goto b;
                        }
                        //jika isCheck null maka akan di execute
                        else
                        {
                            cmdIN.ExecuteNonQuery();
                            Console.WriteLine("\nBarang berhasil ditambah....");
                        }
                        break;

                    //2. Menampilkan semua barang
                    case "2":
                        Console.WriteLine("\n~MENAMPILKAN SEMUA BARANG~ :");

                        //membuat variable untuk mendapatkan data dari table DetailBarang
                        string strQuerySelect = "SELECT * FROM DetailBarang";
                        //membuat command cmdSEL untuk mengeksekusi sql query
                        SqlCommand cmdSEL = new SqlCommand(strQuerySelect, con);
                        //membuat DR data reader untuk mengeksekusi hasil apa pun yang ditetapkan dengan beberapa baris / kolom 
                        SqlDataReader DR = cmdSEL.ExecuteReader();
                        Console.WriteLine();

                        while (DR.Read())
                        {
                            Console.Write(" Kode Faktur : {0}\n " +
                                "Nama Barang : {1}\n " +
                                "Harga:{2}\n " +
                                "Jumlah : {3}\n\n",
                            DR["kd_faktur"], DR["nama_barang"], DR["harga"], DR["jumlah"]);
                        }
                        break;

                    //3. Update Barang
                    case "3":
                        Console.WriteLine("\n~UPDATE BARANG~");
                        //untuk memilih kd_faktur yng ingin di update
                        Console.Write("Masukkan kode faktur yang akan di update :");
                        string up = Console.ReadLine();
                        //untuk mengisi data baru barang 
                        Console.Write("Kode faktur yang baru: ");
                        string kd_fktr = Console.ReadLine();
                        Console.Write("nama barang yang baru: ");
                        string nm_brg = Console.ReadLine();
                        Console.Write("harga yang baru: ");
                        string hrg = Console.ReadLine();
                        Console.Write("Jumlah Baru : ");
                        string jml = Console.ReadLine();

                        //membuat variable untuk mengupdate dari table DetailBarang 
                        string strQueryUpdate = "UPDATE DetailBarang SET kd_faktur='" + kd_fktr+"', nama_barang='" + nm_brg + "', harga='" + hrg + "', jumlah='"+jml+"'" +
                            "WHERE kd_faktur ='" + up + "'";
                        //membuat command mengeksekusi sql query dari variable strQueryUpdate
                        SqlCommand cmdUP = new SqlCommand(strQueryUpdate, con);
                        //membuat command cmd untuk mengeksekusi sql dengan kodisi kd_faktur
                        SqlCommand cmd = new SqlCommand("SELECT kd_faktur FROM DetailBarang WHERE kd_faktur = @kd_faktur", con);
                        //membuat parameter cmd dengan kd_faktur
                        cmd.Parameters.AddWithValue("@kd_faktur", kd_fktr);

                        //membuat variable check untuk mengcek kd_faktur adlah tidak null 
                        //executescalar digunakan karena akan mengembalikan sebuah nilai 
                        var Check = Convert.ToString(cmd.ExecuteScalar()) != "";
                        //jikacheck adalah tidak null maka
                        if (Check)
                        {
                            Console.WriteLine("\nkode faktur telah tersedia...");
                        }
                        else
                        {
                            Console.Write("yakin ingin di update (y/n)? ");
                            string Y = Console.ReadLine();
                            if (Y == "y" || Y == "Y")
                            {
                                //jika check adalah null maka
                                cmdUP.ExecuteNonQuery();
                                Console.WriteLine("\nData berhasil di update...");
                            }
                            else
                            {
                                Console.WriteLine("\nBatal Mengupdate...");
                                Console.WriteLine("Press any key goto menu..");
                                Console.ReadKey();
                                Console.Clear();
                                goto a;
                            }
                        }
                        break;

                    //4. Menghapus Barang
                    case "4":
                        Console.WriteLine("\n~HAPUS BARANG~");
                        Console.Write("Masukkan kode faktur yang akan di hapus : ");
                        string kd_fak = Console.ReadLine();

                        //membuat variable untuk menghapus data sesuai kd_faktur
                        string strQueryDelete = "DELETE DetailBarang WHERE kd_faktur='" + kd_fak + "'";
                        SqlCommand cmdDEL = new SqlCommand(strQueryDelete, con);

                        //kondisi meyakinkan user
                        Console.Write("yakin hapus (y/n)?");
                        string y = Console.ReadLine();
                        if (y == "y" || y == "Y")
                        {
                            //jika setuju maka data akan di hapus
                            cmdDEL.ExecuteNonQuery();
                            Console.WriteLine("\nData berhasil di hapus...");
                        }
                        else
                        {
                            //jika tidak setuju maka akan kembali ke menu
                            Console.WriteLine("Batal Menghapus...");
                            Console.WriteLine("Press any key goto menu..");
                            Console.ReadKey();
                            Console.Clear();
                            goto a;
                        }

                        break;

                    //5. Mencari Barang
                    case "5":
                        x:
                        Console.WriteLine("\n~MENCARI BARANG~");
                        Console.Write("Masukkan Kode Faktur yang ingin di cari :");
                        string kd = Console.ReadLine();

                        //membuat variable untuk mencari data sesuai kd_faktur
                        using (SqlCommand cmdSER = new SqlCommand("SELECT * FROM DetailBarang WHERE kd_faktur = @kd_faktur", con))
                        {
                            //membuat permisalan bahwa kd_faktur == variable kd (saat diinput)
                            cmdSER.Parameters.Add("kd_faktur", System.Data.SqlDbType.VarChar).Value = kd;
                            //untuk membaca  hasil apa pun yang ditetapkan dengan beberapa baris / kolom. dari comand cmdSER
                            using (SqlDataReader dr = cmdSER.ExecuteReader())
                            {
                                //jika memenuhi
                                if (dr.Read())
                                {
                                    Console.WriteLine("\nbarang telah di temukan :");
                                    Console.WriteLine();
                                    Console.Write(" Kode Faktur : {0}\n Nama Barang : {1}\n Harga:{2}\n Jumlah : {3}\n", 
                                        dr["kd_faktur"], dr["nama_barang"], dr["harga"], dr["jumlah"]);
                                }
                                //jika tidak memenuhi
                                else
                                {
                                    Console.WriteLine("\nBarang dengan kode '" + kd + "' tidak di temukan");
                                }
                            }
                        }
                        Console.WriteLine("tekan tombol apapun untuk mencari lagi...");
                        Console.ReadKey();
                        goto x;
                        

                    //6. EXIT PROGRAM
                    case "6":
                        return;

                    //kodisi jika pilihan menu salah
                    default:
                        Console.WriteLine("\nPilihan Salah !!!!");
                        Console.WriteLine("Masukkan pilihan 1-5");
                        goto a;

                }
                //kondisi untuk kembali kemenu 
                Console.Write("\nKembali ke Menu ? (y/n) : ");
                ch = Char.Parse(Console.ReadLine());
                Console.Clear();
                
            } while ((ch == 'y') || (ch == 'Y'));
                
            //menutup koneksi database
            con.Close();
        }
    }
}
