16:32 2014/6/20
1. �����ɿյ�Accdb���ܼ��ϵ��˳���
12:48 2014/6/25
 1. �����µ�Accdb ���Ե���Server�Ѿ����ڵĲ�ָ����TestPlan����
16:55 2014/6/27
1. �޸����ɱ���Accdb�Ƕ�TopoLogRecord ���е�������[��ȡdatatable�Ĳ����޷�ת��ΪMemo]
2. �����û�-��ɫ-���� �������...(�޸ķ�ʽ���޸�Ϊ 0x01...��ʽ)
17:30 2014/6/30
1. ����Type��PN��Ϣά���Ĺ���~[�����޸�OK]
17:35 2014/7/9
1. ��� Login��ʼ
try{}
catch{}
2. ���ɸ�Ϊ2007��accdb
3. ����Ȩ���ж��Ĳ���
4. ����Equip��parameter��Type�ֶ�δ���ò���
10:37 2014/7/10
1. �������л��ֺ�ָ�����ֵ�ĳЩTestPlan����OK
2. ����background �����˺�
3. ��������datagridview����ͷ����Ϊ����
10:42 2014/07/14
1. ��֤�洢���̵���ش���![λ�� Login Form,����δ����(Ϊע��)]
2. //140714_1 Access����2007���ϰ汾 ��2003���°汾
14:20 2014/07/18
1. ����GlobalModelParameter�� PID-->CurrModelID 
2. ����XML�����ļ�!
14:57 2014/07/22
1. �޸�DATAIO�Ĺ�������!
2. ��������//��ȫ�Ĺ������ֿ�ʼ!++++++++++++++++++++++++++++++++++++++
    public class ServerDatabaseIO:DataIO	//SQL���� 2�������ķ���!
	{}
    public class LocalDatabaseIO : DataIO //�������ݿ����� 2�������ķ���!
	{}
15:29 2014/07/25
��ӷ��������� DATAIO��������

15:30 2014/09/11
1. �޸�Login Form��void getLoginStutes()   //140911_0 ����[��ȡ��ǰָ��database���û�����]
2. Login Form����������Դѡ��ť,��������Ȼ�ȡXML�����ļ�,Ȼ����ܽ��е���...
3. BuildXml() ����Ĭ�ϵ�XMLʱ�޸�DBName��������;������� :ATSDBName �� EDVTHome
4. MAINFORM�� refreshAllItem() //140911_2 ����ɾ���󷵻�ˢ�±��������
5. MAINFORM�� btnAppDelete_Click(object sender, EventArgs e) //140911_2 �޸�δ��ʾˢ�µ�����!
6. MAINFORM�� ����btnEquipDelete_Click(object sender, EventArgs e)
7. SQLDataIO.cs����������˳������:
   if (dtDeleted11 != null) da11.Update(dtDeleted11);  //140911_2 ����˳��[ɾ�Ӻ�ɾ��]
   if (dtDeleted10 != null) da10.Update(dtDeleted10);  //140911_2 ����˳��[ɾ�Ӻ�ɾ��]  
17:27 2014/09/16
1. �������ܱ༭ModelInfo���ϵ�BUG  currPrmtrID = Convert.ToInt64( this.dgvPrmtr.CurrentRow.Cells["ID"].Value); //140916_0 �������ܱ༭��BUG  
2. ˢ��ʱ���ԭ������:
	void refreshModelInfo(bool state ) �� ����  clearCboItems();    //140916_1
14:31 2014/09/17
1.  //140917_0 �����������ʱ��ɾ����ť��ȡ������״̬!��ֱ�ӷ��� ����dgvEquipPrmtr.DataSource = null; //140917_0
2.  //140917_0 ������ʾ����()���������ʱ�������ֻ���������ʾ ==[EquipmentForm],����ʱ�������潫������
3.  ��������BUG	//140917_1
4.  ���ݲ�ͬ������Դ,�������ݿ���˺ź����벻ͬ! ���Ե��û�����Ҳ�Ƿֿ���!
10:59 2014/09/18
	1. ��XML�ļ������û�������ļ��ܺͽ��ܲ���!��δ����XML�ļ�,��Ĭ�Ͻ����µ��ļ�(�Ѿ�д���û�������)
	
        /// <summary>
        /// ���м��ܺͽ��ܲ���!!!
        /// </summary>
        /// <param name="str">�����ܻ���ܵ��ַ���!</param>
        /// <param name="decode">����? true =����|false=����</param>
        /// <param name="Codelength">1λ�����λԪ����</param>
        /// <returns>���ؽ�� string</returns>
        protected  string SetPWDCode(string str, bool decode, int Codelength) 
	2. �޸�Ϊ���·��:
	<Compile Include="..\..\CommonLibrary\\XML\XmlIo.cs" />
    	<Compile Include="..\..\CommonLibrary\DataBaseAccess\DataIO.cs" />
2014/09/19
1. ����֧�ֱ������ݿ⹦��,��������ID���������ϵ��ٴ�ȷ��ʱ������������!
2. �� FunctionInfo.form ,RoleInfo.Form,UserInfo.Form,UserRoleFunctionInfo.Form
��DataIO mySqlIO; //140919�޸ĵ�����ѡ�񱾵� or SQL ��ʵ�������� = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   //140917_2    //140911_0
17:18 2014/09/22
1. ��������ҳ��(TypeForm,MSAInfo,MCoefGroup)�ڱ༭�󱣴�ʱ��ʾ"�Ѿ���������"������! �����ж��Ƿ�����������!
2. ʹ�ñ������ݿ�ʱ���� �û�,��ɫ,Ȩ�� ,�û�-��ɫ-Ȩ�� �˵� ����
3. chkSQLlib.Checked = true;   //140922_0 Ĭ��ѡ��SQL ���ݿ�,��Ϊ����! ������ "?"���ǿ�ѡ�������ݿ�
4. ɾ��ԭ�Ȼ�ȡ���ݵ��˻������� ,����ѡ��ָ�����Լƻ�ʱ����������ͣ,��������ʱ�䴥����0
5. ����MSAInfoԭ�ȵĲ��ֵط���������Ϊ16����,�����Ϊ10���Ƶ�����
6. �򱾵�accdb��ȡSQL�Ĳ�����Ҫ����,���������û�������!
10:02 2014/09/23
1. currMSAID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalMSA"], "ID", "ItemName='" + cboMSAName.Text+"'");  //140923_0 Line 231 TypeForm.cs
2. ����BUG:���Ե���ѡ���������ֶ���ѡ�����
3. ����BUG:ɾ���������ͽ��滹��������
4. ����BUG:��ʼ������ ��Ҫ�������ֽ��� ����Ϣ (��ʾ)
9:24 2014/10/09
1. ע�͵���EEPROM��صĳ�ʼ������ά������.
10:53 2014/10/16
1. ������ϵ�в�����PNʱ�޷�����PN �Ĳ���
2. ��EEPROM��ʼ����ṹ����...��ִ�е�������ʱ����...��������
else if (mydt.Columns[j].DataType.ToString().ToUpper().Contains("STRING"))
{
    //141010_0 ��ֹ���ȳ���255 ͳһ��ΪMemo
}

14:56 2014/10/22
��������޸ļ�¼����������Ŀ¼��:
9:33 2014/10/29
1. PN���ӱ���Ϣ���ֶ�datagridview��������С
2. ����MCoefsID ��Ϣ
16:55 2014/10/30
ȡ���������Ϻ�رս����ɾ�����ϲ���,��Ϊ��������ʶ��Ϊfalse<������ɾ����>
16:51 2014/10/31
1. �������ʱ���ټ������û���Ŀ:getLoginStutes()
2. �����ύ����ʧ�ܵ�ErrorLog��¼
11:01 2014/11/04
DefaultLowLimit -->SpecMin
DefaultUpperLimit-->SpecMax
11:05 2014/11/06
1. ת��ΪӢ�Ľ���
2. �Ƴ����ֲ��õĹ���
3. ��ʼ�����ϱ����� ά��'Endianness'

14:47 2014/11/11
1. MASDefine.cs �� MCoefGroup.cs �Ƴ����ϱ��Ϊ�� IDɾ��(ѡ��dgv.Row[ID])
16:02 2014/11/12
1. �޸Ĳ�����־�Ĳ��ִ���ڷ����� ÿ�����ܿ�һ����¼!
2. ���Ӳ���cbo�������б�ֵ
8:54 2014/11/13
1. �������з����������.
14:24 2014/11/19
1. ����ʱ�����ж��Ƿ������ϸı�!
2. ������־���¹����ж��Ƿ����޸�,Ϊ���������ɼ�¼!
3. ��formload�������ݱ��.
15:25 2014/11/25
1. ����������ӻس��Զ�����,�����ж�����������!
2. �����ж��ı�����...
3. �������ComboBoxΪ��������
4. ����TestModel�Ĳ���������λ
10:22 2014/11/26
1. ������־������������ItemNameΪʵ�ʵĹ��ܿ�����