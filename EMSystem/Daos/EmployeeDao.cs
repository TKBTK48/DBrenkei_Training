using EMSystem_CUI.Dtos;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EMSystem_CUI.Daos
{
    public class EmployeeDao : BaseDao<EmployeeDto>
    {
        public EmployeeDao(SqlConnection con) : base(con) { }
        public EmployeeDao(SqlConnection con, SqlTransaction tran) : base(con, tran) { }

        // DB側の名前群
        private const string TABLE_NAME = "employee";
        private const string ID_COLUMN = "id_employee";
        private const string NAME_COLUMN = "nm_employee";
        private const string KANA_COLUMN = "kn_employee";
        private const string MAIL_COLUMN = "mail_address";
        private const string PASS_COLUMN = "password";
        private const string ID_D_COLUMN = "id_department";

        public override string GetTableName()
        {
            return TABLE_NAME;
        }

        public override string GetId()
        {
            return ID_COLUMN;
        }

        /// <summary>
        /// 挿入処理
        /// </summary>
        /// <param name="employee">挿入したいデータをセットするエンティティ</param>
        public void InsertEmployee(EmployeeDto employee)
        {
            /* 
             * StringBuilderでSQL作成
             * 性能は良いが、ちょい見にくくなるし書くのが大変
             * AppendLineで書くと改行されて見やすくなる
             */
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO " + this.GetTableName());
            sql.AppendLine("(");
            sql.AppendLine("     nm_employee");
            sql.AppendLine("    ,kn_employee");
            sql.AppendLine("    ,mail_address");
            sql.AppendLine("    ,password");
            sql.AppendLine("    ,id_department");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("     @name");
            sql.AppendLine("    ,@kana");
            sql.AppendLine("    ,@mail");
            sql.AppendLine("    ,@pass");
            sql.AppendLine("    ,@dep");
            sql.AppendLine(")");

            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(sql.ToString(), this.Con);
                cmd.Transaction = SqlTran;

                SetParameter(cmd, "@name", employee.NmEmployee);
                SetParameter(cmd, "@kana", employee.KnEmployee);
                SetParameter(cmd, "@mail", employee.MailAddress);
                SetParameter(cmd, "@pass", employee.Password);
                SetParameter(cmd, "@dep", employee.IdDepartment);

                // SQL実行
                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Dispose();
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="id">更新したいID</param>
        /// <param name="employee">挿入したいデータをセットするエンティティ</param>
        public void UpdatetEmployee(int id, EmployeeDto employee)
        {
            string sql = $@"
                UPDATE {this.GetTableName()}
                SET
                     { NAME_COLUMN} = @name
                    ,{ KANA_COLUMN} = @kana
                    ,{ MAIL_COLUMN} = @mail
                    ,{ PASS_COLUMN} = @pass
                    ,{ ID_D_COLUMN} = @dep
                WHERE
                    {ID_COLUMN} = @id
            ";

            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(sql.ToString(), this.Con);
                cmd.Transaction = this.SqlTran;

                SetParameter(cmd, "@id", id);
                SetParameter(cmd, "@name", employee.NmEmployee);
                SetParameter(cmd, "@kana", employee.KnEmployee);
                SetParameter(cmd, "@mail", employee.MailAddress);
                SetParameter(cmd, "@pass", employee.Password);
                SetParameter(cmd, "@dep", employee.IdDepartment);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Dispose();
            }
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <param name="id">削除するID</param>
        public void DeleteEmployee(int id)
        {
            string sql = $@"
                DELETE FROM {this.GetTableName()}
                WHERE
                    {ID_COLUMN} = @id
            ";

            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(sql.ToString(), this.Con);
                cmd.Transaction = SqlTran;

                SetParameter(cmd, "@id", id);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Dispose();
            }
        }

        /// <summary>
        /// 1レコードの値からEmployeeDtoのインスタンスを生成する
        /// </summary>
        /// <param name="row">1レコードのデータ</param>
        /// <returns>EmployeeDtoのインスタンス</returns>
        protected override EmployeeDto RowMapping(SqlDataReader reader)
        {
            EmployeeDto emp = new EmployeeDto();

            emp.IdEmployee = (int)reader[ID_COLUMN];
            emp.NmEmployee = (string)reader[NAME_COLUMN];
            emp.KnEmployee = (string)reader[KANA_COLUMN];
            emp.MailAddress = (string)reader[MAIL_COLUMN];
            emp.Password = (string)reader[PASS_COLUMN];
            emp.IdDepartment = (int)reader[ID_D_COLUMN];

            return emp;
        }
    }
}
