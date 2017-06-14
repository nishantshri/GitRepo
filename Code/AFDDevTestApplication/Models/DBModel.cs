using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;


namespace AFDDevTestApplication.Models
{
    public class DBModel
    {
        private string connString = string.Empty;
        private string _property = "Nishant";
        private string _action = "order created";
        private string _status = "Uploaded";
        private string _hash = string.Empty;
        private int _totalCSVRec = 0;
        private int _totalCSVBadRec = 0;
        private int _totalCSVBlankRec = 0;

        public DBModel()
        {
            connString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        }

        public int DeleteCustomerData()
        {
            int count = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand("spDeleteCustomerData", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@RecordCount", SqlDbType.Int));
                    command.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    conn.Open();
                    command.ExecuteNonQuery();
                    count = Convert.ToInt32(command.Parameters["@RecordCount"].Value);
                    return count;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<CustomerModel> GetCustomerData(int flag, string name , int? value=0)
        {
            var customerModel = new List<CustomerModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand("spGetCustomerData", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@flag", SqlDbType.Int).Value = flag;
                    if (flag == 2)
                        command.Parameters.Add("@SEARCH", SqlDbType.VarChar, 50).Value = name;
                    if(flag == 3)
                        command.Parameters.Add("@VALUE", SqlDbType.Int).Value = value.Value;
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    DataSet dataSet = new DataSet();
                    da.Fill(dataSet);
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                       
                        customerModel.Add(new CustomerModel
                        {
                            CustomerName = dataRow[0].ToString(),
                            Value = Convert.ToInt32(dataRow[1])
                        });
                    }
                }
                return customerModel;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public CustomerModel GetCustomerData(int flag, int value)
        {
            var customerModel = new CustomerModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand("spGetCustomerData", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@flag", SqlDbType.Int).Value = flag;                    
                    if (flag == 3)
                        command.Parameters.Add("@VALUE", SqlDbType.Int).Value = value;
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    DataSet dataSet = new DataSet();
                    da.Fill(dataSet);
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        customerModel.Property = dataRow[0].ToString();
                        customerModel.CustomerName = dataRow[1].ToString();
                        customerModel.Value = Convert.ToInt32(dataRow[2]);
                        customerModel.Action = dataRow[3].ToString();
                        customerModel.File = dataRow[4].ToString();
                        customerModel.Status = dataRow[5].ToString();
                        customerModel.Hash = dataRow[6].ToString();
                    }
                }
                return customerModel;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GetCustomerData(int? flag = 1)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand("spGetCustomerData", conn);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@flag", SqlDbType.Int).Value = flag;
                    conn.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    return false;


                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }

        public MessageModel ProcessCSV(string fileName, string fname)
        {
            var messageModel = new MessageModel();
            var flag = true;
            string msg = string.Empty;
            DataTable dt = new DataTable();
            dt = GetDataTable(fileName, fname);
            int dtRowcount = dt.Rows.Count;
            string connString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (var copy = new SqlBulkCopy(conn))
                {
                    conn.Open();

                   

                    copy.DestinationTableName = "Customer";
                    copy.BatchSize = dtRowcount;
                    try
                    {
                        copy.WriteToServer(dt);
                    }
                    catch (Exception ex)
                    {

                        msg = ex.Message;
                        flag = false;
                    }
                }
            }
            msg = String.Format("File Uploaded : {0} \n Total rows in CSV File  : {1} \n Total Bad records in CSV File : {2} \n Total Blank records in CSV File : {3} \n Total Records Processed : {4}  ", fname, _totalCSVRec, _totalCSVBadRec, _totalCSVBlankRec, dtRowcount);
            msg = msg.Replace("\n", "<br/>");
            messageModel.Message = msg;
            messageModel.Flag = flag;
            return messageModel;
        }

        public DataSet GetAllCustomerData()
        {
            string connString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("select * from Customer order by Value", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet myrec = new DataSet();
                da.Fill(myrec);
                return myrec;
            }
        }

        public List<CustomerModel> GetCustomerList()
        {
            var dataSet = new DataSet();
            var customerModel = new List<CustomerModel>();
            try
            {
                dataSet = GetAllCustomerData();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                   
                    customerModel.Add(new CustomerModel
                    {
                        Property = dataRow[0].ToString(),
                        CustomerName = dataRow[1].ToString(),
                        Value = Convert.ToInt32(dataRow[2]),
                        Action = dataRow[3].ToString(),
                        File = dataRow[4].ToString(),
                        Status = dataRow[5].ToString(),
                        Hash = dataRow[6].ToString()
                    });
                }
                return customerModel;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public bool UpdateResponse(Response response)
        {
            bool flag = true;
            try
            {
               
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    
                    SqlCommand cmd = new SqlCommand("spUpdateCustomerData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = response.added;
                    cmd.Parameters.Add("@Hash", SqlDbType.VarChar, 50).Value = response.hash;
                    cmd.Parameters.Add("@Value", SqlDbType.Int).Value = response.value;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                   
                }
            }
            catch (Exception)
            {

                throw;
            }
            return flag;
        }

        public CustomerStatus GetCustomerStatus()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("spGetCustomerStatus", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                command.Parameters["@totalCount"].Direction = ParameterDirection.Output;
                command.Parameters.Add(new SqlParameter("@statusTrue", SqlDbType.Int));
                command.Parameters["@statusTrue"].Direction = ParameterDirection.Output;
                conn.Open();
                command.ExecuteNonQuery();
                var customerStatus = new CustomerStatus();
                customerStatus.totalCount = Convert.ToInt32(command.Parameters["@totalCount"].Value);
                customerStatus.totalUpdate = Convert.ToInt32(command.Parameters["@statusTrue"].Value);
                return customerStatus;

               
            }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        private DataTable GetDataTable(string fileName, string fname)
        {
            DataTable dataTable = new DataTable();
            StreamReader sr = new StreamReader(fileName);

            dataTable.Columns.AddRange(new DataColumn[7] { 
            new DataColumn("Property",typeof(string)),
            new DataColumn("CustomerName", typeof(string)),
            new DataColumn("Value", typeof(int)),
            new DataColumn("Action", typeof(string)),
            new DataColumn("File",typeof(string)),
            new DataColumn("Status",typeof(string)),
            new DataColumn("Hash",typeof(string)) });
            
            string line = string.Empty;
            DataRow row;
                

            while ((line = sr.ReadLine()) != null)
            {
                _totalCSVRec++;
                if (string.IsNullOrEmpty(line))
                {
                    _totalCSVBlankRec++;
                    continue;
                }
                row = dataTable.NewRow();
                var templine = _property + ',' + line + ',' + _action + ',' + fname + ',' + _status + ',' + _hash;
               
                try
                {
                    row.ItemArray = templine.Split(',');
                    dataTable.Rows.Add(row);
                }
                catch (Exception)
                {
                    _totalCSVBadRec++;
                    continue;
                }

            }
            sr.Dispose();

            return dataTable;

        }
    }
}