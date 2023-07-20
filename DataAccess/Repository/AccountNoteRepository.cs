using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccess.ViewModels;

namespace DataAccess.Repository
{
   public class AccountNoteRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public int UpsertNote(AccountNoteModel accountNote)
        {
            int newNoteId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters(accountNote);
                _params.Add("@NewAccountNoteId", DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("Account_Note_Upsert", _params, commandType: CommandType.StoredProcedure);
                newNoteId = _params.Get<int>("NewAccountNoteId");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newNoteId = 0;            

        }

        public bool DeleteNote(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@AccountNoteId", Id);
                connection();
                con.Open();
                con.Execute("Account_Note_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AccountNoteModel GetNoteById(int accountNoteId)
        {
            try
            {
                connection();
                con.Open();
                AccountNoteModel AccountList = con.Query<AccountNoteModel>("Account_Note_Fetch", new { AccountNoteId = accountNoteId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return AccountList;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        //public List<dynamic> GetAccountNotes(int accountId)
        //{
        //    try
        //    {
        //        connection();
        //        con.Open();
        //        IList<dynamic> accountNoteList = con.Query<dynamic>("Account_Note_FetchAllAccount", new { AccountId= accountId }, commandType: CommandType.StoredProcedure).ToList();          
        //        con.Close();
        //        return accountNoteList.ToList();
        //    }
        //    catch (Exception exe)
        //    {
        //        throw;
        //    }
        //}

        public List<dynamic> GetAccountActivity(int accountId)
        {
            try
            {
                connection();
                con.Open();
                IList<dynamic> accountNoteList = con.Query<dynamic>("Account_Activity_FetchAll", new { AccountId = accountId }, commandType: CommandType.StoredProcedure).ToList();
                con.Close();
                return accountNoteList.ToList();
            }
            catch (Exception exe)
            {
                throw;
            }
        }

        public int UpsertDocument(AccountDocumentViewModel accountDocument,out string docName)
        {
            int newAccountDocId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters(accountDocument);
                _params.Add("@NewAccountDocId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                _params.Add("@DocName", dbType: DbType.String, direction: ParameterDirection.Output,size:500);

                con.Execute("Account_Doc_Upsert", _params, commandType: CommandType.StoredProcedure);
                newAccountDocId = _params.Get<int>("NewAccountDocId");
                docName = _params.Get<string>("DocName");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newAccountDocId = 0;

        }

        public bool DeleteDocument(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@AccountDocId", Id);
                connection();
                con.Open();
                con.Execute("Account_Doc_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
