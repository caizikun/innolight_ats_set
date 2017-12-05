16:32 2014/6/20
1. 将生成空的Accdb功能集合到此程序
12:48 2014/6/25
 1. 生成新的Accdb 可以导入Server已经存在的并指定的TestPlan资料
16:55 2014/6/27
1. 修改生成本机Accdb是对TopoLogRecord 进行单独处理[获取datatable的部分无法转换为Memo]
2. 新增用户-角色-功能 管理界面...(修改方式待修改为 0x01...方式)
17:30 2014/6/30
1. 增加Type和PN信息维护的功能~[本地修改OK]
17:35 2014/7/9
1. 检查 Login开始
try{}
catch{}
2. 生成改为2007的accdb
3. 修正权限判定的部分
4. 修正Equip的parameter的Type字段未启用部分
10:37 2014/7/10
1. 导出所有机种和指定机种的某些TestPlan功能OK
2. 新增background 连接账号
3. 新增部分datagridview的列头翻译为中文
10:42 2014/07/14
1. 验证存储过程的相关代码![位于 Login Form,但是未启用(为注释)]
2. //140714_1 Access区分2007以上版本 和2003以下版本
14:20 2014/07/18
1. 修正GlobalModelParameter的 PID-->CurrModelID 
2. 新增XML配置文件!
14:57 2014/07/22
1. 修改DATAIO的公共部分!
2. 新增子类//完全的公共部分开始!++++++++++++++++++++++++++++++++++++++
    public class ServerDatabaseIO:DataIO	//SQL子类 2个公共的方法!
	{}
    public class LocalDatabaseIO : DataIO //本地数据库子类 2个公共的方法!
	{}
15:29 2014/07/25
添加服务器引用 DATAIO公共部分

15:30 2014/09/11
1. 修改Login Form的void getLoginStutes()   //140911_0 方法[获取当前指定database的用户数量]
2. Login Form的新增数据源选择按钮,当点击后先获取XML配置文件,然后才能进行登入...
3. BuildXml() 建立默认的XML时修改DBName子项资料;变更属性 :ATSDBName 和 EDVTHome
4. MAINFORM中 refreshAllItem() //140911_2 修正删除后返回刷新报错的问题
5. MAINFORM中 btnAppDelete_Click(object sender, EventArgs e) //140911_2 修复未显示刷新的问题!
6. MAINFORM中 新增btnEquipDelete_Click(object sender, EventArgs e)
7. SQLDataIO.cs中修正更新顺序问题:
   if (dtDeleted11 != null) da11.Update(dtDeleted11);  //140911_2 调整顺序[删子后删主]
   if (dtDeleted10 != null) da10.Update(dtDeleted10);  //140911_2 调整顺序[删子后删主]  
17:27 2014/09/16
1. 修正不能编辑ModelInfo资料的BUG  currPrmtrID = Convert.ToInt64( this.dgvPrmtr.CurrentRow.Cells["ID"].Value); //140916_0 修正不能编辑的BUG  
2. 刷新时清除原有资料:
	void refreshModelInfo(bool state ) 中 新增  clearCboItems();    //140916_1
14:31 2014/09/17
1.  //140917_0 新增误点新增时按删除按钮则取消新增状态!并直接返回 并将dgvEquipPrmtr.DataSource = null; //140917_0
2.  //140917_0 修正显示问题()当误点新增时参数部分还有资料显示 ==[EquipmentForm],新增时参数界面将被锁定
3.  修正部分BUG	//140917_1
4.  根据不同的数据源,连接数据库的账号和密码不同! 各自的用户密码也是分开的!
10:59 2014/09/18
	1. 对XML文件新增用户和密码的加密和解密部分!若未发现XML文件,在默认建立新的文件(已经写入用户和密码)
	
        /// <summary>
        /// 进行加密和解密操作!!!
        /// </summary>
        /// <param name="str">待加密或解密的字符串!</param>
        /// <param name="decode">解密? true =解密|false=加密</param>
        /// <param name="Codelength">1位密码的位元长度</param>
        /// <returns>返回结果 string</returns>
        protected  string SetPWDCode(string str, bool decode, int Codelength) 
	2. 修改为相对路径:
	<Compile Include="..\..\CommonLibrary\\XML\XmlIo.cs" />
    	<Compile Include="..\..\CommonLibrary\DataBaseAccess\DataIO.cs" />
2014/09/19
1. 新增支持本地数据库功能,修正部分ID在新增资料的再次确认时表名错误问题!
2. 将 FunctionInfo.form ,RoleInfo.Form,UserInfo.Form,UserRoleFunctionInfo.Form
的DataIO mySqlIO; //140919修改到根据选择本地 or SQL 来实例化对象 = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   //140917_2    //140911_0
17:18 2014/09/22
1. 修正部分页面(TypeForm,MSAInfo,MCoefGroup)在编辑后保存时提示"已经存在资料"的问题! 增加判断是否是新增内容!
2. 使用本地数据库时不将 用户,角色,权限 ,用户-角色-权限 菜单 隐藏
3. chkSQLlib.Checked = true;   //140922_0 默认选择SQL 数据库,且为隐藏! 当按下 "?"键是可选本地数据库
4. 删除原先获取数据的账户和密码 ,并在选择指定测试计划时将计数器暂停,新增部分时间触发置0
5. 修正MSAInfo原先的部分地方输入资料为16进制,保存后为10进制的问题
6. 因本地accdb获取SQL的部分需要解码,新增解码用户和密码!
10:02 2014/09/23
1. currMSAID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalMSA"], "ID", "ItemName='" + cboMSAName.Text+"'");  //140923_0 Line 231 TypeForm.cs
2. 修正BUG:可以单独选择新增机种而不选择类别
3. 修正BUG:删除机种类型界面还存在资料
4. 修正BUG:初始化资料 需要新增大字节序 的信息 (显示)
9:24 2014/10/09
1. 注释掉与EEPROM相关的初始化资料维护代码.
10:53 2014/10/16
1. 修正当系列不存在PN时无法新增PN 的部分
2. 因EEPROM初始化表结构更改...在执行导出数据时报错...进行修正
else if (mydt.Columns[j].DataType.ToString().ToUpper().Contains("STRING"))
{
    //141010_0 防止长度超过255 统一改为Memo
}

14:56 2014/10/22
新增存放修改记录到本机程序目录下:
9:33 2014/10/29
1. PN的子表信息部分对datagridview重新填充大小
2. 修正MCoefsID 信息
16:55 2014/10/30
取消新增资料后关闭界面的删除资料步骤,改为将新增标识置为false<避免误删资料>
16:51 2014/10/31
1. 登入程序时不再检查登入用户数目:getLoginStutes()
2. 增加提交数据失败的ErrorLog记录
11:01 2014/11/04
DefaultLowLimit -->SpecMin
DefaultUpperLimit-->SpecMax
11:05 2014/11/06
1. 转换为英文界面
2. 移除部分不用的功能
3. 初始化资料表增加 维护'Endianness'

14:47 2014/11/11
1. MASDefine.cs 和 MCoefGroup.cs 移除资料变更为按 ID删除(选择dgv.Row[ID])
16:02 2014/11/12
1. 修改操作日志的部分存放于服务器 每个功能块一条记录!
2. 增加部分cbo的下拉列表值
8:54 2014/11/13
1. 修正换行符号输入错误.
14:24 2014/11/19
1. 更新时增加判断是否有资料改变!
2. 操作日志更新功能判定是否有修改,为空则不再生成记录!
3. 将formload部分内容变更.
15:25 2014/11/25
1. 登入界面增加回车自动登入,增加判定密码错误次数!
2. 增加判定文本长度...
3. 变更部分ComboBox为单独下拉
4. 增加TestModel的参数描述栏位
10:22 2014/11/26
1. 操作日志修正部分名称ItemName为实际的功能块名称