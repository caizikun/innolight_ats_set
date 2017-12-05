using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///BasePage 的摘要说明
/// </summary>
public class BasePage : System.Web.UI.Page
{
  public  struct TestPlanAuthoriy
    {
      public  string planid;
      public bool modifyPlan;
      public bool deletePlan;
      public bool runPlan;
    }
	public BasePage()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public void IsSessionNull()
    {
        try
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Default.aspx", true);
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public void SetSessionBlockType(UInt16 bolockid)
    {
        Session["BlockType"] = bolockid;
    }
}