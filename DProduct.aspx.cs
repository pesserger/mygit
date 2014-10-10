using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
public partial class DProduct : System.Web.UI.Page
{
    string sjbh;
    string sjip;
    string sjmc;
    string dalei = "";
    string xiaolei = "";
    string bianhao = "";
    string type = "0";
    protected Mobancom1444 mbdata;
    chaxuncanshu canshu;
    string frompage = "";
    string ifcx = "";
    string ifcxDa = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        sjbh = Request.QueryString["sjbh"];
        sjip = Request.QueryString["sjip"];
        sjmc = Request.QueryString["sjmc"];
        dalei = Request.QueryString["dalei"];
        xiaolei = Request.QueryString["xiaolei"];
        bianhao = Request.QueryString["bianhao"];
        yansebh.Value = Request.QueryString["yansebh"];
        chimabh.Value = Request.QueryString["chimabh"];
        string t = Request.QueryString["type"];
        if(!String.IsNullOrEmpty(t))
        type =t;
        mbdata = new Mobancom1444(HttpContext.Current, Page.Page, Server, sjmc, sjbh, sjip);
        canshu = new chaxuncanshu();
        canshuxs.Value = canshu.xianshi1 + "@" + canshu.xianshi2 + "@" + canshu.xianshi3 + "@" + canshu.xianshi4;
        hdfzhekou.Value=MoBanCom20140324.getzhk(sjbh).ToString();
        {
            LoadData();

        }
        string[] np = mbdata.getnextpre(bianhao, xiaolei, mbdata.frompage(), "1020", mbdata);
        prenext.Text = np[0] + np[1];
        if (!IsPostBack)
        {
            string mn =Ifjiazai();
            if (mn.Equals("1"))
            {
                mbdata.RenQi(bianhao);
            }
        }

    }


    protected string Ifjiazai()
    {
        string ret = "1", danury = "", shangyigeurl = "";
        danury = (string)HttpContext.Current.Request.Url.ToString().Trim();
        //Response.Write(Request.UrlReferrer+"jjjjjj");
        try
        {
            shangyigeurl = HttpContext.Current.Request.UrlReferrer.ToString().Trim();
            if (danury.Equals(shangyigeurl))
            {
                ret = "0";
            }
        }
        catch (Exception)
        {
                ret = "1";
           // Response.Write(danury+"ffffffff");
            //Response.Write(shangyigeurl+"ttttttttt");

        }
        return ret;
    }

    private void LoadData()
    {
        string imgurl = "", imgurl2 = "", imgurl3 = "", imgurl4 = "", imgurl5 = "";
        double zhekou2 = Lab.GetJine(hdfzhekou.Value);
        string ygbh = (string)Session["ygbh"];
        int ckcount = mbdata.cangku();
        if (ckcount == 0)
        {
            divgoumai.Style.Add("display", "none");
            divnomai.Visible = true;
        }
        string ss;
        DataTable dt = mbdata.GetHunGood(bianhao, ygbh, type,out ss);
        string shujukubh = mbdata.shujkbh();
        string lujing = "/uploadfile/" + shujukubh + @"/zaixian/chuchuang/";
        string[] shxx = mbdata.GetSongHuo();
        string sh = "";
        if ("1".Equals(shxx[0]))
            sh = "<span style='margin-right:30px'><img src='../images/shsm.png' style='width:20px;vertical-align: text-bottom;margin-right: 3px;'>送货上门</span>";
        if ("1".Equals(shxx[1]))
            sh = sh + "<span><img src='../images/hdfk.png' style='width:20px;vertical-align:middle;margin-top: -2px;margin-right: 3px;'>见货付款</span>";
        gtsh.Text=sh;
        StringBuilder strbl = new StringBuilder();
        switch (type)
        {
            case "0":
                putong.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    hdfId.Value = dr["id"].ToString();
                    hidDwMoRen.Value = dr["danwei_moren"].ToString();
                    lblspmingcheng.Text = dr["mingcheng"].ToString();
                    Page.Title = lblspmingcheng.Text;
                    ifcx = dr["if_cx"].ToString().Trim();
                    ifcxDa = dr["if_cxDa"].ToString().Trim();
                    lblguige.Text = dr["guige"].ToString();
                   // ltlmiaoshu.Text = dr["miaoshu"].ToString().Replace("src=\"", "src=\"" + sjip);
                    lbltuijian.Text = dr["tuijianmsg"].ToString();
                    double cxjia = Lab.GetJine(dr["cxjia"].ToString());
                    xiaodanwei.Text=hiddanwei.Value = dr["danwei"].ToString().Trim();
                    dadanwei.Text=hidDwFz.Value = dr["danwei_fuzhu"].ToString().Trim();
                    hdfBl.Value = dr["bili"].ToString().Trim();
                    bili.Text = "1*" + hdfBl.Value;
                    lblguige.Text = dr["guige"].ToString().Trim();
                    pricenamexj.Text = pricenamedj.Text = canshu.xianshi1;
                    pricenamexy.Text = pricenamedy.Text = canshu.xianshi2;
                    if ("1".Equals(dr["if_cx"].ToString().Trim()))
                    {
                        xdanweijia.Text = (Double.Parse(dr["shoujia_da_geren"].ToString().Trim())).ToString("f2");
                        pxjy.Visible = false;
                        pricenamexj.Text = canshu.xianshi4;
                    }
                    else
                        xdanweijia.Text = (Double.Parse(dr["shoujia_da_geren"].ToString().Trim()) * zhekou2).ToString("f2");

                    if (!("0".Equals(dr["cxjia"].ToString().Trim())))
                    {
                        if ("1".Equals(dr["if_cx"].ToString().Trim()))
                            xdanweijia.Text = (Double.Parse(dr["cxjia"].ToString().Trim())).ToString("f2");
                        else
                            xdanweijia.Text = (Double.Parse(dr["cxjia"].ToString().Trim()) * zhekou2).ToString("f2");

                    }
                    xdanweiyuan.Text =  Double.Parse(dr["shoujia_da_geren"].ToString().Trim()).ToString("f2");
                    if ("1".Equals(dr["if_cxDa"].ToString().Trim()))
                    {
                        dadanweijia.Text = (Double.Parse(dr["shoujia_huiyuan"].ToString().Trim())).ToString("f2");
                        pdjy.Visible = false;
                        pricenamedj.Text = canshu.xianshi4;
                    }
                    else
                        dadanweijia.Text = (Double.Parse(dr["shoujia_huiyuan"].ToString().Trim()) * zhekou2).ToString("f2");
                    dadanweiyuan.Text =  Double.Parse(dr["shoujia_huiyuan"].ToString().Trim()).ToString("f2");
                    if (!IsPostBack)
                    {
                        ddldanwei.Items.Add(new ListItem(hiddanwei.Value, hiddanwei.Value));
                        if (hidDwFz.Value.Length > 0 && !("0.00".Equals(dadanweijia.Text)))
                        {
                            ddldanwei.Items.Add(new ListItem(hidDwFz.Value, hidDwFz.Value));
                        } 
                    }
                    dadanweisheng.Text = (Double.Parse(dadanweiyuan.Text.ToString()) - Double.Parse(dadanweijia.Text.ToString())).ToString("f2");
                    xdanweisheng.Text = (Double.Parse(xdanweiyuan.Text.ToString()) - Double.Parse(xdanweijia.Text.ToString())).ToString("f2");

                    if (!IsPostBack)
                    {
                        if ("1".Equals(hidDwMoRen.Value))
                        {
                            hdfyuanjia.Value = dadanweiyuan.Text;
                            ddldanwei.SelectedIndex = 1;
                        }
                        else
                        {
                            hdfyuanjia.Value = xdanweiyuan.Text;

                        }

                    }
                    #region // 图片加载
                    if (dr["imgurl"].ToString().Length > 0)
                    {
                        imgurl = sjip + lujing + dr["imgurl"].ToString();
                        ltldatu.Text = "<a href=\"" + imgurl + "\" id=\"Zoomer\" class=\"MagicZoomPlus\" rel=\"selectors-effect:false;zoom-fade:true;thumb-change:mouseover\" onclick=\" return false;\"><img src=\"" + imgurl + "\" alt=\"\" id=\"goodsimg\" /></a>";
                        strbl.Append("<li><a href=\"" + imgurl + "\" rel=\"zoom-id:Zoomer\" rev=\"" + imgurl + "\" title=\"\">");
                        strbl.Append("<img src=\"" + imgurl + "\" alt=\"\" class=\"onbg\"  width=\"350\" /></a></li>");

                    }
                    if (dr["imgurl1"].ToString().Length > 0)
                    {
                        imgurl2 = sjip + lujing + dr["imgurl1"].ToString();
                        strbl.Append("<li><a href=\"" + imgurl2 + "\" rel=\"zoom-id:Zoomer\" rev=\"" + imgurl2 + "\" title=\"\">");
                        strbl.Append("<img src=\"" + imgurl2 + "\" alt=\"\" class=\"autobg\"  /></a></li>");

                    }
                    if (dr["imgurl2"].ToString().Length > 0)
                    {
                        imgurl3 = sjip + lujing + dr["imgurl2"].ToString();
                        strbl.Append("<li><a href=\"" + imgurl3 + "\" rel=\"zoom-id:Zoomer\" rev=\"" + imgurl3 + "\" title=\"\">");
                        strbl.Append("<img src=\"" + imgurl3 + "\" alt=\"\" class=\"autobg\"  /></a></li>");

                    }
                    if (dr["imgurl3"].ToString().Length > 0)
                    {
                        imgurl4 = sjip + lujing + dr["imgurl3"].ToString();
                        strbl.Append("<li><a href=\"" + imgurl4 + "\" rel=\"zoom-id:Zoomer\" rev=\"" + imgurl4 + "\" title=\"\">");
                        strbl.Append("<img src=\"" + imgurl4 + "\" alt=\"\" class=\"autobg\"  /></a></li>");

                    }
                    if (dr["imgurl4"].ToString().Length > 0)
                    {
                        imgurl5 = sjip + lujing + dr["imgurl4"].ToString();
                        strbl.Append("<li><a href=\"" + imgurl5 + "\" rel=\"zoom-id:Zoomer\" rev=\"" + imgurl5 + "\" title=\"\">");
                        strbl.Append("<img src=\"" + imgurl5 + "\" alt=\"\" class=\"autobg\"  /></a></li>");

                    }
                    ltlxiaotu.Text = strbl.ToString();
                    #endregion
                }
                break;
            case "1":
                fuzhuang.Visible = true;
                ddldanwei.Visible = false;
                fzjpn.Text = canshu.xianshi1;
                fzypn.Text = canshu.xianshi2;
                string yuanjia = "";
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    hdfId.Value = dr["id"].ToString();
                    lblspmingcheng.Text = dr["mingcheng"].ToString();
                    Page.Title = lblspmingcheng.Text;
                    hiddanwei.Value = dr["danwei"].ToString();
                    dw.Text = hiddanwei.Value;
                    ifcx = dr["if_cx"].ToString().Trim();
                    ifcxDa = dr["if_cxDa"].ToString().Trim();
                    yuanjia = dr["yuanjia"].ToString().Trim();
                    //ltlmiaoshu.Text = dr["miaoshu"].ToString().Replace("src=\"", "src=\"" + sjip);
                    lbltuijian.Text = dr["tuijianmsg"].ToString();
                    double cxjia = Lab.GetJine(dr["cxjia"].ToString());
                    fzjiage.Text = (Double.Parse(dr["shoujia_da_geren"].ToString().Trim()) * zhekou2).ToString("f2");
                    fzyuanj.Text = (Double.Parse(dr["shoujia_da_geren"].ToString().Trim()) ).ToString("f2");
                    #region // 图片加载
                    if (dr["imgurl"].ToString().Length > 0)
                    {
                        imgurl = sjip + lujing + dr["imgurl"].ToString();
                        ltldatu.Text = "<a href=\"" + imgurl + "\" id=\"Zoomer\" class=\"MagicZoomPlus\" rel=\"selectors-effect:false;zoom-fade:true;thumb-change:mouseover\" onclick=\" return false;\"><img src=\"" + imgurl + "\" alt=\"\" id=\"goodsimg\" /></a>";
                        strbl.Append("<li><a href=\"" + imgurl + "\" rel=\"zoom-id:Zoomer\" rev=\"" + imgurl + "\" title=\"\">");
                        strbl.Append("<img src=\"" + imgurl + "\" alt=\"\" class=\"onbg\"  width=\"350\" /></a></li>");

                    }
                    if (dr["imgurl1"].ToString().Length > 0)
                    {
                        imgurl2 = sjip + lujing + dr["imgurl1"].ToString();
                        strbl.Append("<li><a href=\"" + imgurl2 + "\" rel=\"zoom-id:Zoomer\" rev=\"" + imgurl2 + "\" title=\"\">");
                        strbl.Append("<img src=\"" + imgurl2 + "\" alt=\"\" class=\"autobg\"  /></a></li>");

                    }
                    if (dr["imgurl2"].ToString().Length > 0)
                    {
                        imgurl3 = sjip + lujing + dr["imgurl2"].ToString();
                        strbl.Append("<li><a href=\"" + imgurl3 + "\" rel=\"zoom-id:Zoomer\" rev=\"" + imgurl3 + "\" title=\"\">");
                        strbl.Append("<img src=\"" + imgurl3 + "\" alt=\"\" class=\"autobg\"  /></a></li>");

                    }
                    if (dr["imgurl3"].ToString().Length > 0)
                    {
                        imgurl4 = sjip + lujing + dr["imgurl3"].ToString();
                        strbl.Append("<li><a href=\"" + imgurl4 + "\" rel=\"zoom-id:Zoomer\" rev=\"" + imgurl4 + "\" title=\"\">");
                        strbl.Append("<img src=\"" + imgurl4 + "\" alt=\"\" class=\"autobg\"  /></a></li>");

                    }
                    if (dr["imgurl4"].ToString().Length > 0)
                    {
                        imgurl5 = sjip + lujing + dr["imgurl4"].ToString();
                        strbl.Append("<li><a href=\"" + imgurl5 + "\" rel=\"zoom-id:Zoomer\" rev=\"" + imgurl5 + "\" title=\"\">");
                        strbl.Append("<img src=\"" + imgurl5 + "\" alt=\"\" class=\"autobg\"  /></a></li>");

                    }
                    ltlxiaotu.Text = strbl.ToString();
                    #endregion
                }
                #region  服装类
                StringBuilder strchima = new StringBuilder();
                StringBuilder stryanse = new StringBuilder();
                StringBuilder stryscm = new StringBuilder();
                int yscount = 0, cmcount = 0;//颜色数量；尺码数量
                DataTable dtysjia = mbdata.Getproperty(bianhao);
                    //if (dtysjia.Rows.Count <= 0)
                    //{
                    //    divchima.Visible = false;
                    //    divyanse.Visible = false;
                    //}
                    string jiage = "";
                    double comp = 9999999999999999999;
                    foreach (DataRow drys in dtysjia.Rows)
                    {
                        cmgname.Text = drys["cmgn"].ToString();
                        ysgname.Text = drys["ysgn"].ToString();
                        //查出来的该颜色中  的编号，名称，尺码的编号、名称 以及数量  拼串传值；
                        stryscm.Append(drys["yanse_bh"] + "|" + drys["yanse_mc"] + "|" + drys["chima_bh"] + "|" + drys["chima_mc"] + "|" + drys["shuliang"] + "@");
                        //如果是这个尺码编号存在这个尺码组中，加载span
                        if (!strchima.ToString().Contains("<i>" + drys["chima_bh"].ToString()))
                        {
                            strchima.Append(@"<li><span  class=provalue  id=li_chima" + drys["chima_bh"] + " onclick=\"sltchima('" + cmcount + "','$count','" + drys["chima_bh"] + "','" + drys["chima_mc"] + "','" + jiage + "')\">" + (drys["chima_mc"].ToString().Length == 0 ? "&nbsp;" : drys["chima_mc"].ToString()) + "<i>" + drys["chima_bh"] + "</i></span></li>");
                            cmcount++;//
                        }
                        if (!stryanse.ToString().Contains("<i>" + drys["yanse_bh"].ToString()))
                        {
                            jiage = drys["jiage_xfz"].ToString();
                            if (jiage.Length == 0)
                            {
                                jiage = yuanjia;
                            }
                            else
                            {
                                jiage = Lab.GetJine(jiage).ToString("f2");
                            }
                            stryanse.Append("<li><span id=\"li_yanse" + drys["yanse_bh"] + "\" class=\"provalue\" onclick=\"sltyanse('" + yscount + "','$count','" + drys["yanse_bh"] + "','" + drys["yanse_mc"] + "','" + jiage + "')\">" + (drys["yanse_mc"].ToString().Length == 0 ? "&nbsp;" : drys["yanse_mc"].ToString()) + "<i>" + drys["yanse_bh"] + "</i></span></li>");
                            if (comp > Lab.GetJine(jiage)) { 
                                comp = Lab.GetJine(jiage);
                                which.Value = drys["yanse_bh"].ToString(); 
                                fzjiage.Text = (Lab.GetJine(jiage) * zhekou2).ToString("f2");
                                fzyuanj.Text = jiage;
                                if(!IsPostBack)
                                hdfyuanjia.Value = jiage;
                            }
                            yscount++;
                        }
                    }
                ltrchima.Text = strchima.ToString().Replace("$count", cmcount.ToString());
                ltryanse.Text = stryanse.ToString().Replace("$count", yscount.ToString());
                if (stryscm.Length > 0)//颜色尺码
                {
                    hdfyscm.Value = stryscm.ToString().Substring(0, stryscm.Length - 1);
                }

                #endregion

                if (sjbh.Substring(sjbh.Length-2).Equals("96"))
                {
                     dtysjia = mbdata.Getpropertylc(bianhao,"2");
                     comp = Lab.GetJine(fzyuanj.Text);
                    foreach (DataRow drys in dtysjia.Rows)
                    {
                        yscount++;
                        ysgname.Text = drys["ysgn"].ToString();
                        jiage = drys["jiage_xfz"].ToString();
                        if (jiage.Length == 0)
                        {
                            jiage = fzyuanj.Text;
                        }
                        else
                        {
                            jiage = Lab.GetJine(jiage).ToString("f2");
                        }

                        if (comp > Lab.GetJine(jiage))
                        {
                            comp = Lab.GetJine(jiage);
                            which.Value = drys["yanse_bh"].ToString();
                            fzjiage.Text = (Lab.GetJine(jiage) * zhekou2).ToString("f2");
                            fzyuanj.Text = jiage;
                            if (!IsPostBack)
                                hdfyuanjia.Value = jiage;
                        }
                        stryanse.Append("<li><span id=\"li_yanse" + drys["yanse_bh"] + "\" class=\"provalue\" onclick=\"sltyanselc('" + yscount + "','$count','" + drys["yanse_bh"] + "','" + drys["yanse_mc"] + "','" + jiage + "')\">" + (drys["yanse_mc"].ToString().Length == 0 ? "&nbsp;" : drys["yanse_mc"].ToString()) + "<i>" + drys["yanse_bh"] + "</i></span></li>");
                    }
                    ltryanse.Text = stryanse.ToString().Replace("$count", yscount.ToString());
                    dtysjia = mbdata.Getpropertylc(bianhao, "1");
                    foreach (DataRow drys in dtysjia.Rows)
                    {
                        cmcount++;
                        strchima.Append("<li><span id=\"li_chima" + drys["chima_bh"] + "\" class=\"provalue\" onclick=\"sltchimalc('" + cmcount + "','" + drys["chima_bh"] + "','" + drys["chima_mc"] +  "')\">" + (drys["chima_mc"].ToString().Length == 0 ? "&nbsp;" : drys["chima_mc"].ToString()) + "<i>" + drys["chima_bh"] + "</i></span></li>");
                        cmgname.Text = drys["group_mc"].ToString();
                    }
                    ltrchima.Text = strchima.ToString();

                }
                if(String.IsNullOrEmpty(yansebh.Value))
                ClientScript.RegisterStartupScript(this.GetType(), "", "$(function(){$('#li_yanse" + which.Value + "').click()});", true);
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "", "$(function(){selyscm('" + yansebh.Value + "','" + chimabh.Value + "')});", true);


                break;
            case "2":
                tushu.Visible = true;
                ddldanwei.Visible = false;
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    hdfId.Value = dr["id"].ToString();
                    hidDwMoRen.Value = dr["danwei_moren"].ToString();
                    lblspmingcheng.Text = dr["mingcheng"].ToString();
                    ifcx = dr["if_cx"].ToString().Trim();
                    ifcxDa = dr["if_cxDa"].ToString().Trim();
                    Page.Title = lblspmingcheng.Text;
                    double cxjia = Lab.GetJine(dr["cxjia"].ToString());
                    //ltlmiaoshu.Text = dr["miaoshu"].ToString().Replace("src=\"", "src=\"" + sjip);
                    lbltuijian.Text = dr["tuijianmsg"].ToString() ;
                    tspjn.Text = canshu.xianshi1;
                    tspyn.Text = canshu.xianshi2;
                    hiddanwei.Value = dr["danwei"].ToString();
                    dw.Text = hiddanwei.Value;
                    if ("1".Equals(dr["if_cx"].ToString().Trim()))
                    {
                        tsjiage.Text = (Double.Parse(dr["shoujia_da_geren"].ToString().Trim())).ToString("f2");
                        tsjy.Visible = false;
                        tspjn.Text = canshu.xianshi4;
                    }
                    else
                        tsjiage.Text = (Double.Parse(dr["shoujia_da_geren"].ToString().Trim()) * zhekou2).ToString("f2");

                    if (!("0".Equals(dr["cxjia"].ToString().Trim())))
                    {
                        if ("1".Equals(dr["if_cx"].ToString().Trim()))
                            tsjiage.Text = (Double.Parse(dr["cxjia"].ToString().Trim())).ToString("f2");
                        else
                            tsjiage.Text = (Double.Parse(dr["cxjia"].ToString().Trim()) * zhekou2).ToString("f2");
                    }
                    tsyuanj.Text= Double.Parse(dr["shoujia_da_geren"].ToString().Trim()).ToString("f2");
                    zuozhe.Text = dr["zuozhe"].ToString().Trim();
                    chubanshe.Text = dr["chubanshe"].ToString().Trim();
                    chbtime.Text = dr["chubansj"].ToString().Trim();
                    isbn.Text = dr["isbn"].ToString().Trim();
                    if (!IsPostBack)
                    {
                        hdfyuanjia.Value = tsyuanj.Text;
                    }
                    tssheng.Text = (Double.Parse(tsyuanj.Text.ToString()) - Double.Parse(tsjiage.Text.ToString())).ToString("f2");
                    #region // 图片加载
                    if (dr["imgurl"].ToString().Length > 0)
                    {
                        imgurl = sjip + lujing + dr["imgurl"].ToString();
                        ltldatu.Text = "<a href=\"" + imgurl + "\" id=\"Zoomer\" class=\"MagicZoomPlus\" rel=\"selectors-effect:false;zoom-fade:true;thumb-change:mouseover\" onclick=\" return false;\"><img src=\"" + imgurl + "\" alt=\"\" id=\"goodsimg\" /></a>";
                        strbl.Append("<li><a href=\"" + imgurl + "\" rel=\"zoom-id:Zoomer\" rev=\"" + imgurl + "\" title=\"\">");
                        strbl.Append("<img src=\"" + imgurl + "\" alt=\"\" class=\"onbg\"  width=\"350\" /></a></li>");

                    }
                    if (dr["imgurl1"].ToString().Length > 0)
                    {
                        imgurl2 = sjip + lujing + dr["imgurl1"].ToString();
                        strbl.Append("<li><a href=\"" + imgurl2 + "\" rel=\"zoom-id:Zoomer\" rev=\"" + imgurl2 + "\" title=\"\">");
                        strbl.Append("<img src=\"" + imgurl2 + "\" alt=\"\" class=\"autobg\"  /></a></li>");

                    }
                    if (dr["imgurl2"].ToString().Length > 0)
                    {
                        imgurl3 = sjip + lujing + dr["imgurl2"].ToString();
                        strbl.Append("<li><a href=\"" + imgurl3 + "\" rel=\"zoom-id:Zoomer\" rev=\"" + imgurl3 + "\" title=\"\">");
                        strbl.Append("<img src=\"" + imgurl3 + "\" alt=\"\" class=\"autobg\"  /></a></li>");

                    }
                    if (dr["imgurl3"].ToString().Length > 0)
                    {
                        imgurl4 = sjip + lujing + dr["imgurl3"].ToString();
                        strbl.Append("<li><a href=\"" + imgurl4 + "\" rel=\"zoom-id:Zoomer\" rev=\"" + imgurl4 + "\" title=\"\">");
                        strbl.Append("<img src=\"" + imgurl4 + "\" alt=\"\" class=\"autobg\"  /></a></li>");

                    }
                    if (dr["imgurl4"].ToString().Length > 0)
                    {
                        imgurl5 = sjip + lujing + dr["imgurl4"].ToString();
                        strbl.Append("<li><a href=\"" + imgurl5 + "\" rel=\"zoom-id:Zoomer\" rev=\"" + imgurl5 + "\" title=\"\">");
                        strbl.Append("<img src=\"" + imgurl5 + "\" alt=\"\" class=\"autobg\"  /></a></li>");

                    }
                    ltlxiaotu.Text = strbl.ToString();
                    #endregion
                }
                break;
        }
        if (string.IsNullOrEmpty(ifcx)) ifcx = "0";
        if (string.IsNullOrEmpty(ifcxDa)) ifcxDa = "0";

        string lm_id = mbdata.daleiid(bianhao);
        StringBuilder strlei = new StringBuilder();
        DataTable dsxiao = new DataTable();
        if (!string.IsNullOrEmpty(lm_id))
            dsxiao = mbdata.XiaoIds(lm_id);
        if (dsxiao.Rows.Count > 0)
        {
            strlei.Append("<span class=\"smallfl\"><a href=\"/program/zaixian/1020/spxinxi.aspx?sjbh=" + sjbh + "&sjmc=" + sjmc + "&sjip=" + sjip + "&dalei=" + lm_id + @"&xiaolei=" + "" + "\" title=\"\" >全部</a></span>");
            foreach (DataRow dr in dsxiao.Rows)
            {
                if (dr["xl_id"].ToString().Trim().Equals(Request.QueryString["xiaolei"]))
                    strlei.Append("<span class=\"smallfl\"><a class=\"cur\" href=\"/program/zaixian/1020/spxinxi.aspx?sjbh=" + sjbh + "&sjmc=" + sjmc + "&sjip=" + sjip + "&dalei=" + lm_id + @"&xiaolei=" + dr["xl_id"].ToString().Trim() + "\" title=\"\">" + dr["xl_rnm"] + "</a></span>");
                else
                    strlei.Append("<span class=\"smallfl\"><a href=\"/program/zaixian/1020/spxinxi.aspx?sjbh=" + sjbh + "&sjmc=" + sjmc + "&sjip=" + sjip + "&dalei=" + lm_id + @"&xiaolei=" + dr["xl_id"].ToString().Trim() + "\" title=\"\">" + dr["xl_rnm"] + "</a></span>");

            }
        }
        else
        {
            strlei.Append("<span class=\"smallfl\"><a href=\"/program/zaixian/1020/spxinxi.aspx?sjbh=" + sjbh + "&sjmc=" + sjmc + "&sjip=" + sjip + "&dalei=" + lm_id + @"&xiaolei=" + "" + "\" class=\"cur\" >全部</a></span>");

        }
        flmcheng.Text = mbdata.daleimingcheng(lm_id);
        ltllei.Text = strlei.ToString();
        hdfifcx.Value = ifcx;
        hdfifcxDa.Value = ifcxDa;

    }
    protected void IBtngoumai_Click(object sender, EventArgs e)
    {
        string name = lblspmingcheng.Text;
        string id = hdfId.Value;
        string url = Request.FilePath + "?bianhao=" + bianhao + "&sjbh=" + sjbh + "&sjmc=" + sjmc + "&sjip=" + sjip;
        if (Session["ygbh"] == null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('登录后才能操作！');window.location='/denglu.aspx?url=" + url + "'", true);
            return;
        }
        double jiage ;
        double number=Lab.GetJine(tbshuliang.Text);
        string danwei = hiddanwei.Value;
        string guige = lblguige.Text;
        string danWeiFuZhu = hidDwFz.Value;
        double biLi = Lab.GetJine(hdfBl.Value);
        switch (type)
        {
            case "0":
                jiage = Lab.GetJine(hdfyuanjia.Value);
                string selectDw = ddldanwei.SelectedValue;
                string ifda = "0";
                if (selectDw == hidDwFz.Value )
                {
                    ifda = "1";
                    ifcx = ifcxDa;
                }
                Session["goumai"] = id + "@" + bianhao + "@" + name + "@" + jiage + "@" + number + "@" + danwei + "@" + guige + "@" + biLi + "@" + danWeiFuZhu + "@" + ifda + "@" + ifcx;
                break;
            case "1":
                jiage = Lab.GetJine(hdfyuanjia.Value);
                //Response.Write(jiage);
                Session["goumai"] = id + "@" + bianhao + "@" + name + "@" + jiage + "@" + number + "@" + danwei + "@" + selyanseid.Value + "@" + selyansemc.Value + "@" + selchimaid.Value + "@" + selchimamc.Value + "@" + ifcx+ "@fz";

                break;
            case "2":
                jiage = Lab.GetJine(hdfyuanjia.Value);
                Session["goumai"] = id + "@" + bianhao + "@" + name + "@" + jiage + "@" + number + "@" + danwei + "@" + "" + "@" + biLi + "@" + "" + "@" + "" + "@" + ifcx;
                break;
        }
       Response.Redirect("/program/zaixian/chanpin/Jiesuan.aspx?dizhi=goumai&sjbh=" + sjbh + "&sjip=" + sjip + "&sjmc=" +Server.UrlEncode(sjmc));
    }
    protected void IBtnjrgwc_Click(object sender, EventArgs e)
    {
      
        string name = lblspmingcheng.Text;
        string id = hdfId.Value;
        string url = Request.FilePath + "?bianhao=" + bianhao + "&sjbh=" + sjbh + "&sjmc=" + sjmc + "&sjip=" + sjip;
        if (Session["ygbh"] == null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('登录后才能操作！');window.location='/denglu.aspx?url=" + url + "'", true);
            return;
        }
        double number = Lab.GetJine(tbshuliang.Text);
        string ss="";
        double jiage; string danwei; string guige; string danWeiFuZhu;
        danwei = hiddanwei.Value;
        switch (type)
        {
            case "0":
                jiage = Lab.GetJine(hdfyuanjia.Value);
                guige = lblguige.Text;
                danWeiFuZhu = hidDwFz.Value;
                double biLi = Lab.GetJine(hdfBl.Value);
                string selectDw = ddldanwei.SelectedValue;
                string ifda = "0";
                if (selectDw == hidDwFz.Value)
                {
                    ifda = "1";
                    ifcx = ifcxDa;
                }
               ss = GouWu(name, jiage, id, number, danwei, guige, biLi, danWeiFuZhu, ifda,"","","","");
                break;
            case "1":
                jiage = Lab.GetJine(hdfyuanjia.Value);                
                ss = GouWu(name, jiage, id, number, danwei, "", 1, "","", selyanseid.Value, selchimaid.Value,selyansemc.Value, selchimamc.Value);
                break;
            case "2":
                jiage = Lab.GetJine(hdfyuanjia.Value);
                ss = GouWu(name, jiage, id, number, danwei, "", 1, "", "", "", "", "", "");
                break;
        }
        ss = ss.Trim();
        if (ss == "true")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "dddd", "$(function(){gouwuche()})", true);
        }           
    }

    private string GouWu(string name, double price, string id, double number, string danwei, string guige, double biLi, string danWeiFuZhu, string ifda, string ysbh, string cmbh, string ysmc, string cmmc)
    {
        string ygbh = (string)Session["ygbh"];
        string ygip = (string)Session["ygip"];
        string sqlcunzai="";
        switch (type)
        {
            case "0":
                sqlcunzai = "select count(1) from ding_gouwu where ygbh='" + ygbh + "' and sjbh='" + sjbh + "' and ycl_bh='" + bianhao + "' and if_da='" + ifda + "'" + " and danjia=" + price + " and if_cx='" + ifcx + "'";
                      break;
            case "1":
                      sqlcunzai = "select count(1) from ding_gouwu where ygbh='" + ygbh + "' and sjbh='" + sjbh + "' and ycl_bh='" + bianhao + "' and yanse_bh='" + ysbh + "' and chima_bh='" + cmbh + "'" + " and danjia=" + price + " and if_cx='" + ifcx + "'";
                      break;
            case "2":
                      sqlcunzai = "select count(1) from ding_gouwu where ygbh='" + ygbh + "' and sjbh='" + sjbh + "' and ycl_bh='" + bianhao + "' and if_da='" + ifda + "'" + " and danjia=" + price + " and if_cx='" + ifcx+"'";
                      break;

        }
        string ifcunzi = DataSelect.selectstring(sqlcunzai, DataSelect.dataname);
        string []sqlszj=new string[1];
        if (ifcunzi == "1")
        {
            switch(type)
            {
                case "0":
                    sqlszj[0] = "update ding_gouwu set danjia=" + price + ",shuliang=shuliang+" + number + ",djsj='" + DateTime.Now.ToString() + "' where ygbh='" + ygbh + "' and sjbh='" + sjbh + "' and ycl_bh='" + bianhao + "' and if_da='" + ifda + "'";
                    break;
                case "1":
                    sqlszj[0] = "update ding_gouwu set danjia=" + price + ",shuliang=shuliang+" + number + ",djsj='" + DateTime.Now.ToString() + "' where ygbh='" + ygbh + "' and sjbh='" + sjbh + "' and ycl_bh='" + bianhao + "' and yanse_bh='" + ysbh + "' and chima_bh='" + cmbh + "'";
                    break;
                case "2":
                    sqlszj[0] = "update ding_gouwu set danjia=" + price + ",shuliang=shuliang+" + number + ",djsj='" + DateTime.Now.ToString() + "' where ygbh='" + ygbh + "' and sjbh='" + sjbh + "' and ycl_bh='" + bianhao + "' and if_da='" + ifda + "'";
                    break;


            }
        }
        else
        {
            switch (type)
            {
                case "0":
                    sqlszj[0] = "insert into ding_gouwu(ygbh,ycl_bh,ycl_mc,guige,danwei,shuliang,danwei_fuzhu,bili,danjia,if_da,djsj,sjbh,sjmc,sjip,if_cx) " +
                        "values('" + ygbh + "','" + bianhao + "','" + name + "','" + guige + "','" + danwei + "','" + number + "','" + danWeiFuZhu + "','" + biLi + "','" + price + "','" + ifda + "','" + DateTime.Now.ToString() + "','" + sjbh + "','" + sjmc + "','" + sjip + "','" +ifcx+ "')";
                    break;
                case "1":
                    sqlszj[0] = "insert into ding_gouwu(ygbh,ycl_bh,ycl_mc,danwei,shuliang,danjia,yanse_bh,yanse_mc,chima_bh,chima_mc,djsj,sjbh,sjmc,sjip,qita_shangpin,if_cx) " +
                        "values('" + ygbh + "','" + bianhao + "','" + name + "','" + danwei + "','" + number + "','" + price + "','" + ysbh + "','" + ysmc + "','" + cmbh + "','" + cmmc + "','" + DateTime.Now.ToString() + "','" + sjbh + "','" + sjmc + "','" + sjip + "','1',"+"'"+ifcx+"')";
                    break;
                case "2":
                    sqlszj[0] = "insert into ding_gouwu(ygbh,ycl_bh,ycl_mc,guige,danwei,shuliang,danwei_fuzhu,bili,danjia,if_da,djsj,sjbh,sjmc,sjip,if_cx) " +
                        "values('" + ygbh + "','" + bianhao + "','" + name + "','" + guige + "','" + danwei + "','" + number + "','" + danWeiFuZhu + "','" + biLi + "','" + price + "','" + ifda + "','" + DateTime.Now.ToString() + "','" + sjbh + "','" + sjmc + "','" + sjip + "','" + ifcx + "')";
                    break;
            }
        }
        return DataSelect.CMD_Array(sqlszj, DataSelect.dataname);
    }
////////////////////////////////////
}
