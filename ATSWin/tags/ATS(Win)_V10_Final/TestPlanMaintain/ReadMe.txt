16:14 2014/6/18
1. ����CopyTestPlan�Ĺ��� [��Ҫѡ��һ�����е�TestPlan]
   �����ɵ�TestPlan��Ҫ����Ƿ����и�����!
   �����ɵ�TestEquipment��[ItemName]����Ҫ��������
2. ���ݵ�����Excel��������,��ͷ�ļ��޸�Ϊ����!�Ҳ���ʾ ID,PID,MGropuID...
3. ���ݿ�database·����� Ϊ 10.160.20.106\ATS_Home
4. GC.Collect();   //140618_2 ֻ��PNInfo.form�Ĳ��ֽ����ڴ����.
15:38 2014/6/19
1. ȷ��Copy���Լƻ�����OK!
2. �������Լƻ���Execl ���ܶ���Ŀ�Ĳ�����Ҫ���� TBD
11:19 2014/6/20
1. ����"�������Լƻ�"�ı�ͷ���ù���,ȷ��OK.
2. "�������Լƻ�"�ı���ʽ��ȷ��!
16:59 2014/6/20
1. �������µ�Accdb�����Ƴ�
15:46 2014/6/24
1. ��һ���ձ�������¼ʱϵͳ��ȡ�������һ������ID���ֽ����޸�...
14:17 2014/6/25
1. �������� conn.Close(); ���ж���������

16:19 2014/7/1
1. MGroupID--> MCoefsID
2. �޸ı���Ϊ: GlobalManufactureCoefficientsGroup	(ԭΪ"GlobalManufactureMemoryGroupTable")
3. �޸ı���Ϊ: GlobalManufactureCoefficients	(ԭΪ"GlobalManufactureMemory")
4. �޸�PN�������Ϣ��ʾ"..." ��ʾPN�µ�4������Ϣ!
11:13 2014/7/3
1. �����Ƴ��豸�б�TestModel BUG
2. ����AccessDB���������й��� 
3. TestPlan form��ʾ���PNʱˢ�����ϲ���BUG & 
4. ��TestModel��Ϊ����������ʾParameter
14:23 2014/7/4.
1. ����XML�����ļ�
2. SQLManager ���캯������ dbName ����
13:09 2014/7/7
1. ��������Global�������豸���ֽ���ȷ������!  //140707_0
17:09 2014/7/9
1. TestCtrl��TestPlan �����޸�����(˫�����ƺ�������øù���),�������������ϵ������ظ����,���ظ����������,����ᵼ��ɾ��������!
2. TestCtrl�������б������󲿷�Ĭ��ֵ����(�Ե�ǰ���ݿ���ڵ�Ϊ�ο�)[���������ݲ�֧��]
3. �����޸����빦��
4. ����ҳ������ѡ�����ݺ���ʾ��ǰ������Ϣ(����ά��ʱ����)
5. ����datagridview����������ʾ����,Ĭ�������������ʾ����
16:14 2014/07/14
1. ����ҳ������ �ж�currlst.SelectedIndex != -1   //140714_0
2. �����޸ĳɹ��������������
3. dgv�����ж� if (dgvEquipPrmtr.CurrentRow != null && dgvEquipPrmtr.CurrentRow.Index != -1) //140714_0
4. �豸���� �����ȴ��� 15 ���Զ������и�
5. mySqlIO = new AccessManager(Login.AccessFilePath);  //140714_1 ���캯���޸�
6. ����+ģ��+������ʾ�Ĳ��ֶ�ˢ�½��д��� RefreshMyInfo(bool isFormLoad)

17:19 2014/07/16
1. ��Excel������Ϊÿ����һ��sheet!
2. Login form��timer���ڵ�������ʱ���״̬������
14:22 2014/07/22
1. �޸�DATAIO�Ĺ�������!
2. ��������//��ȫ�Ĺ������ֿ�ʼ!++++++++++++++++++++++++++++++++++++++
    public class ServerDatabaseIO:DataIO	//SQL���� 2�������ķ���!
	{}
    public class LocalDatabaseIO : DataIO //�������ݿ����� 2�������ķ���!
	{}
15:19 2014/07/25
DATAIO�������� GetCurrTime
�����Ϊ���÷�������������

17:28 2014/09/12	//140912_0 
1. �޸�Login Form��void getLoginStutes()   //140912_0 ����[��ȡ��ǰָ��database���û�����]
2. Login Form����������Դѡ��ť,��������Ȼ�ȡXML�����ļ�,Ȼ����ܽ��е���...
3. BuildXml() ����Ĭ�ϵ�XMLʱ�޸�DBName��������;������� :ATSDBName �� EDVTDBName
4. ��PNInfo �� Login Form��txt���� ��ʾ��ǰ������Դ (ATSHome|EDVTHome)

11:05 2014/09/18
��XML�ļ������û�������ļ��ܺͽ��ܲ���!��δ����XML�ļ�,��Ĭ�Ͻ����µ��ļ�(�Ѿ�д���û�������)
17:30 2014/10/21. 
���ӱ��������޸�Log��Ϣ�Ĳ���[�ϴ���Server�Ĳ�����Ҫ����]
12:32 2014/10/22
1. MaintainPlan/CtrlInfo.cs : ���Ӷ��������Եĳ����ж���<=0 ������
2. ����UserLoginInfo ���ŵ�����޸ļ�¼...
3. ���Ӳ��ַ�������...[ModelInfo]
4. SQLDataIO.cs -->����ʱ���¼��㵱ǰ���豸��ź��豸����
5. PNInfo-->  TopoToatlDSAcceptChanges()���Ӽ�¼�޸ĵ�����

15:15 2014/10/28
1. �����豸��� �豸��ʼ��˳��[���ݿ�ĿǰĬ����1,��������ʽ������Զ���SEQ��Ϊ1-N]
2. ctrl��Model�������ֶ�chkIgnoreFlag<������Ե�ǰ��Ŀ
3. ���������豸����[�����ڵ�ǰTestPlanΨһ]
9:13 2014/10/29
1. ��TRX ��ΪNA...
2. ȡ������ǰǿ�Ƽ���豸������ID��Ӧ�Ĳ���
3.  ȡ���������ѡ�����ݿ�����[SQL]�Ĳ���,ֱ�Ӵ�xml�ļ���ȡ
10:20 2014/10/30
Equipment �����ֶ�"Role" ��� 0:NA,1:TX,2:RX
��Ӧ�Ĳ�ѯ�������Ϊ: ItemName+PID+SEQ,����ɾ���������Ϊ���� ID;
12:42 2014/10/30
�ı�����豸˳��ķ�����filterStr2��˳��������
void ChangeSEQ(int direction, ListBox myList, DataTable dt, string filterStr1, string filterStr2)
10:43 2014/10/31
1. ȡ��PNInfo.cs ��Login.cs�в���Ҫ��ע�Ͳ���
2. �޸��������������ʾ��ʽ:myPNInfo.ShowDialog();
3. ��Ϊ�޸����豸����:�����ظ�����ɾ,�޸Ĳ�ѯ����
4. �������ֽ���δ�������ɾ�����ϵĲ��ָ�Ϊ��������ʶ��Ϊfalse;
14:04 2014/10/31
�޸�ǰ������ϼ�¼ID��
if (ss1.Length <= 0) ss1 += "�޸�ǰ����Ϊ:" + "ID=" + dataRow["ID"].ToString();
if (ss2.Length <= 0) ss2 += "�޸�ǰ����Ϊ:" + "ID=" + dataRow["ID"].ToString();

16:49 2014/10/31
1. �������ʱ���ټ������û���Ŀ:getLoginStutes()
2. �����ύ����ʧ�ܵ�ErrorLog��¼
11:01 2014/11/04
DefaultLowLimit -->SpecMin
DefaultUpperLimit-->SpecMax

Equipment��TestParameter���ӵ���ر�ʱ��ʾȷ���뿪ѡ��?
11:04 2014/11/06
1.ת��ΪӢ�Ľ���
2. testplan ��Ϊ����ɾ��!���Ǻ���

9:31 2014/11/12
1. ������¼��testplan�ֱ��¼! ������ڱ�:[OperationLogs]
15:23 2014/11/12
1. ��������Ӣ�Ļ�����.
14:21 2014/11/19
1. ���Ӵ������־���޸�ʱ�� �Ҳ�����־Ϊ�հ�ʱ���ٴ浵.
2. ����ʱ�����ж��Ƿ������ϸı�!
3. ��formload�������ݱ��.
16:14 2014/11/25
1. ����������ӻس��Զ�����,�����ж�����������!
2. �����ж��ı�����...
3. �������ComboBoxΪ��������
4. ����TestModel�Ĳ���������λ
10:54 2014/11/28
1. ������ȡTestPlan PID��ϵ��PIDѭ����������������!
2. �༭������TestPlan ��FlowControl���ж���ʽ���!

13:16 2014/12/03
1. ��Login�����Ϊͳһ��������[��Ҫͬ���޸�Config.XML����],�����ռ�ͳһ��Ϊ :Maintain
2. �������ֱ����ķ������η�Ϊ private,ͳһ�������ʾ�Ĵ���Ϊ :MainForm
14:12 2014/12/04
����������Դ����TestPlan�Ĺ���![����Դ֧��Accdb+SQLServer]
9:12 2014/12/09
1. ����������©��Ӣ����ʾ
2. TestModelParameter��Ϊ�������ʱ����Ԥ��������嵥[0,1]
10:24 2014/12/11
����TestModelParameter SpecMax=32767��SpecMin=-32768 ������м�������Ƿ����!
15:18 2014/12/18
1. Log��¼�Ĳ���:�����޸�TestModel��TestParameterʱҲ��¼��ǰ��FlowCtrl����<����鿴>
2. Equip ��������ʱ˥����ȷ�������ATTSlot��ʽ
3. �汾����Ϊ1.2.0
14:05 2014/12/25
ѡ��Type����"..."��ť������ʾType�Ĳ���MSA��Ϣ!
ѡ��PN����"..."��ť,������ʾPN��Ϣ!
9:50 2015/01/04
1.TopoTestPlan�������ֶ� [IsChipInitialize],�����ж��Ƿ���Ҫװ�ó�ʼ��;
  TopoTestPlan��������[IsChipInit](����ǰ������ֶ� [IsChipInitialize]����ʾ,������ʾ)
  ,���ڱ�������ʱ���ټ��Auxδ��дֵ.
16:08 2015/01/14
1. �Ż��ڴ洢������־�Ĳ��ֲ�ѯ����
2. ��������ʾ���������TestPlan������TestPlan������������
3. TestPlan��������������ά���Ƿ���Ҫ����ϵ������.
15:10 2015/01/15
��֤Ȩ�޲����޸�...
9:59 2015/01/19
ͬ���޸�:
1. �޸�����: �������� Microsoft.Office.Interop.Access;
   ������ADOX������
9:24 2015/02/03
1. �����汾Ϊ1.0.2.2
2. ��Config.XML��key���Ͻ��д���,��ֹ Ϊ null �Ĳ��ֵ��±���!
3. ��PN��"..."��Type��"..."������������page,addr��ʾ!���������û���������!
16:20 2015/02/03
1. �µ�ATSDebug���ݿ�ܹ����֧����Ϣά������
 a. ����GlobalMSAEEPROMInitializeΪ TopoMSAEEPROMSet.
 b. TopoManufactureConfigInit.PID= TopoTestPlan.ID
 c. TopoTestPlan �����ֶ�IsEEPROMInitialize: �Ƿ���Ҫ��ʼ��ManufactureEEPROM����
 d. ����TestPlan���ϵ�Excel����.
 e. SQLDataIO.cs��ֹ�ձ�ʱ��ȡ������ID��������!
 f. ����TestPlanʱ������û���Ҫ����ѡ����� MConfigInit��ά�� ����Equipmentά��,�����Զ���ת
12:31 2015/02/09
�汾����Ϊ 1.0.2.3
1. ���ӶԲ���datagridview����Ĭ������Ĳ��ֺͼ��̹����¼���Ӧ!
2. ����MConfig�������Ϻñ��潹�������dgv��Ӧ��������.
16:54 2015/02/27
1. ����������TestPlanʱ��ȡTestPlanID���������!
2. ���ֲ�ѯ����ָ�� ID= myLoginID
3. ��������ʱUpdateTable���������ж���ǰdt�Ƿ������ϱ��޸�!
10:52 2015/03/05
�����汾Ϊ: 1.0.3.0
1. TestCtrlForm.cs���ˢ�¶�Ӧ��ʾ����~
2. MainForm �������sslRunMsg�Ϸ�ʱ��ʾ��ǰ��sslRunMsg��Ϣ
15:36 2015/03/25
�����汾Ϊ: 1.0.3.1
1. FLEXDCA����O/Eͨ�������Ƹ���Ϊ:"0,0,0,0"��"1A,1B,1C,1D"��ʽ���Ա���
16:16 2015/04/11
�����汾Ϊ: 1.0.3.2
1. �����˸���ָ��FollowControl�Ĺ���!(���������ݿ��Ѿ����ڵ�����)
2. �������ܿ��Ը���Mdoel�����ֶ�[FailBreak]��ModelParameterɾ���ֶ�[FailBreak];
3. ��ѯType/PNʱ���������ж��Ƿ�����ֶ�[IgnoreFlag]
4. �����豸ʱ��δѡ����豸�Ĺ�����Ԥ��Ϊ"NA";
5. TestPlan,TestCtrl,TestModelҳ����������ItemDescription(TestPlan,TestCtrl:�����ݿ�����ֶ�����ʾ)
10:40 2015/04/14
1. TopoTestControl ���������ֶ�������ǰ�����Ǹ����"FMT/LP/Both",���ݿ�洢Ϊ"2,1,3";