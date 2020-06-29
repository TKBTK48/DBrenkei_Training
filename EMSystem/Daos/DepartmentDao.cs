using EMSystem_CUI.Dtos;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EMSystem_CUI.Daos
{
    public class DepartmentDao : BaseDao<DepartmentDto>
    {
        public DepartmentDao(SqlConnection con) : base(con) { }
        public DepartmentDao(SqlConnection con, SqlTransaction tran) : base(con, tran) { }

        // DB側の名前群
        private const string TABLE_NAME = "department";
        private const string ID_COLUMN = "id_department";
        private const string NAME_COLUMN = "nm_department";

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
        /// <param name="department">挿入したいデータをセットするエンティティ</param>
        public void InsertDepartment(DepartmentDto department)
        {
            /* 
             * StringBuilderでSQL作成
             * 性能は良いが、ちょい見にくくなるし書くのが大変
             * AppendLineで書くと改行されて見やすくなる
             */
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO " + this.GetTableName());
            sql.AppendLine("(");
            sql.AppendLine("     nm_department");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("     @name");
            sql.AppendLine(")");

            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(sql.ToString(), this.Con);
                cmd.Transaction = SqlTran;

                SetParameter(cmd, "@name", department.NmDepartment);

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
        /// <param name="department">挿入したいデータをセットするエンティティ</param>
        public void UpdatetDepartment(int id, DepartmentDto department)
        {
            string sql = $@"
                UPDATE {this.GetTableName()}
                SET
                     { NAME_COLUMN} = @name
                WHERE
                    {ID_COLUMN} = @id
            ";

            var cmd = new SqlCommand();
            try
            { 
                cmd = new SqlCommand(sql.ToString(), this.Con);
                cmd.Transaction = this.SqlTran;

                SetParameter(cmd, "@id", id);
                SetParameter(cmd, "@name", department.NmDepartment);

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
        public void DeleteDepartment(int id)
        {
            string sql = $@"
                DELETE FROM {this.GetTableName()}
                WHERE
                    {ID_COLUMN} = @id
            ";

            var cmd = new SqlCommand();
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
        /// 1レコードの値からDepartmentDtoのインスタンスを生成する
        /// </summary>
        /// <param name="row">1レコードのデータ</param>
        /// <returns>DepartmentDtoのインスタンス</returns>
        protected override DepartmentDto RowMapping(SqlDataReader reader)
        {
            DepartmentDto dept = new DepartmentDto();

            dept.IdDepartment = (int)reader[ID_COLUMN];
            dept.NmDepartment = (string)reader[NAME_COLUMN];

            return dept;
        }
    }
}
