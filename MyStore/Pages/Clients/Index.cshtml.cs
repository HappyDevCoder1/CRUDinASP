using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        //this list stores the data of all clients
        public List<ClientInfo> ListClients = new List<ClientInfo>();

        //fillling the list in OnGet()
        public void OnGet()
        {
            try
            {
                //connecting to the database
                String connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //storing clinet info in the clinet info object
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();

                                //adding clientinfo object to list clients list
                                ListClients.Add(clientInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    //creating clinet info class to store the databse data
    //it store data of one client to store data of all client they needed to be stored in a list
    public class ClientInfo
    {
        public String id;

        public String name;

        public String email;

        public String phone;

        public String address;

        public String created_at;


    }
}
