using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yelp2
{
    public partial class Form1 : Form
    {
       
        string bid;
        string treeselectstring = "";
        List<String> databid;
        List<String> passedbid;
        MySQL_Connection mydb;
        public Form1()
        {
            InitializeComponent();
            mydb = new MySQL_Connection();
        }

      

      private void Form1_Load(object sender, EventArgs e)
        {
            string qstr = "Select distinct state from business order by state;";
            string qstr2 = "select distinct category from categories order by category;";
            List<String> qresult = mydb.SQLSELECTExec(qstr, "state");
            List<String> qrestul2 = mydb.SQLSELECTExec(qstr2, "category");
          for(int i = 0; i < qresult.Count();i++)
          {
              StateComboBox.Items.Add(qresult[i]);
              StateComboBox2.Items.Add(qresult[i]);

          }
          for(int i = 0; i < qrestul2.Count(); i++)
          {
              CategoryListBox.Items.Add(qrestul2[i]);
              CategoryListBox2.Items.Add(qrestul2[i]);
          }
          for (int i = 1; i < 6; i++)
          {
              MinRatingComboBox.Items.Add(i);
              MaxRatingComboBox.Items.Add(i);
          }
         /* for(int i = 50; i <= 500; i+=50)
          {
              
              MinReviewTextBox.Add(i);
          }*/
        /* for( int i = 500; i <= 3000; i+= 500)
         {
             MaxReviewsComboBox.Items.Add(i);
         }*/

        }

      private void StateComboBox_SelectedIndexChanged(object sender, EventArgs e)
      {
          string qstr = "select distinct city from cities where state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' order by city;";
          string qstr2 = "select sum(population) from cities where state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "';";
          string qstr3 = "select avg(Avg_income) from cities where state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "';";
          
          CityListBox.Items.Clear();
          CityListBox.ResetText();
          ZipCodeListBox.Items.Clear();
          ZipCodeListBox.ResetText();
          StatePopBox.Clear();
          StatePopBox.ResetText();
          StateIncomeBox.Clear();
          StateIncomeBox.ResetText();
          CityPopBox.Clear();
          CityPopBox.ResetText();
          CityIncomeBox.Clear();
          CityIncomeBox.ResetText();
          ZipcodePopBox.Clear();
          ZipcodePopBox.ResetText();
          ZipcodeIncomeBox.Clear();
          ZipcodeIncomeBox.ResetText();
          List<String> qresult = mydb.SQLSELECTExec(qstr, "city");
          String qresult2 = mydb.SQLSELECTExec2(qstr2, "population");
          String qresult3 = mydb.SQLSELECTExec2(qstr3, "Avg_income");
          for (int i = 0; i < qresult.Count(); i++)
          {
              CityListBox.Items.Add(qresult[i]);
          }
          StatePopBox.AppendText(qresult2);
          StateIncomeBox.AppendText(qresult3);
      }

      

      private void CityListBox_SelectedIndexChanged(object sender, EventArgs e)
      {
          string qstr = "select distinct zipcode from cities where state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and city = '" + CityListBox.GetItemText(CityListBox.SelectedItem) + "' order by zipcode;";
          string qstr2 = "select sum(population) from cities where state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and city = '" + CityListBox.GetItemText(CityListBox.SelectedItem) + "';";
          string qstr3 = "select avg(Avg_income) from cities where state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and city = '" + CityListBox.GetItemText(CityListBox.SelectedItem) + "';";

          ZipCodeListBox.Items.Clear();
          ZipCodeListBox.ResetText();
          CityPopBox.Clear();
          CityPopBox.ResetText();
          CityIncomeBox.Clear();
          CityIncomeBox.ResetText();
          ZipcodePopBox.Clear();
          ZipcodePopBox.ResetText();
          ZipcodeIncomeBox.Clear();
          ZipcodeIncomeBox.ResetText();
          List<String> qresult = mydb.SQLSELECTExec(qstr, "zipcode");
          String qresult2 = mydb.SQLSELECTExec2(qstr2, "population");
          String qresult3 = mydb.SQLSELECTExec2(qstr3, "Avg_income");
          for (int i = 0; i < qresult.Count(); i++)
          {
              ZipCodeListBox.Items.Add(qresult[i]);
          }
          CityPopBox.AppendText(qresult2);
          CityIncomeBox.AppendText(qresult3);
      }

      private void ZipCodeListBox_SelectedIndexChanged(object sender, EventArgs e)
      {
         string qstr = "select population from cities where state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and city = '" + CityListBox.GetItemText(CityListBox.SelectedItem) + "' and zipcode = '" + ZipCodeListBox.GetItemText(ZipCodeListBox.SelectedItem) + "';";
          string qstr2 = "select avg_income from cities where state = '" +  StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and city = '" + CityListBox.GetItemText(CityListBox.SelectedItem) + "' and zipcode = '" + ZipCodeListBox.GetItemText(ZipCodeListBox.SelectedItem) + "';";

         String qresult = mydb.SQLSELECTExec2(qstr, "population");
         String qresult2 = mydb.SQLSELECTExec2(qstr2, "avg_income");
         ZipcodePopBox.Clear();
         ZipcodeIncomeBox.Clear();
         ZipcodePopBox.AppendText(qresult);
         ZipcodeIncomeBox.AppendText(qresult2);

      }

      private void CategoryListBox_ItemCheck(object sender, ItemCheckEventArgs e)
      {
          CheckedListBox items = (CheckedListBox)sender;
          if(items.CheckedItems.Count > 10)
          {
              e.NewValue = CheckState.Unchecked;
          }
      }

      private void UpdateButton_Click(object sender, EventArgs e)
      {
          dataGridView1.Rows.Clear();
          dataGridView2.Rows.Clear();
          dataGridView3.Rows.Clear();
          if(ZipCodeListBox.SelectedItem != null) //Everything has been selected
          {
              string businessnumstring = "select count(distinct business.bid) from business, categories where business.bid = categories.bid and business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and business.city = '" + CityListBox.GetItemText(CityListBox.SelectedItem) + "' and business.zipcode = '" + ZipCodeListBox.GetItemText(ZipCodeListBox.SelectedItem) + "'";

              string reviewsstring = "select avg(business.reviewcount) from business where business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and business.city = '" + CityListBox.GetItemText(CityListBox.SelectedItem) + "' and business.zipcode = '" + ZipCodeListBox.GetItemText(ZipCodeListBox.SelectedItem) + "'  and business.bid in (select categories.bid from categories where";


              string avgratingstring = "select avg(avgstar) from categories, business_average_reviews, business where categories.bid = business_average_reviews.bid and business.bid = categories.bid and business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and business.city = '" + CityListBox.GetItemText(CityListBox.SelectedItem) + "' and business.zipcode = '" + ZipCodeListBox.GetItemText(ZipCodeListBox.SelectedItem) + "' and ";

              for (int i = 0; i < CategoryListBox.CheckedItems.Count; i++)
              {
                  businessnumstring += " and categories.category = '" + CategoryListBox.CheckedItems[i].ToString() + "';";
                  reviewsstring += " categories.category = '" + CategoryListBox.CheckedItems[i].ToString() + "');";
                  avgratingstring += " categories.category = '" + CategoryListBox.CheckedItems[i].ToString() + "';";


                  String numresult = mydb.SQLSELECTExec2(businessnumstring, "bid");
                  String reviewqresult = mydb.SQLSELECTExec2(avgratingstring, "stars");
                  String avgresult = mydb.SQLSELECTExec2(avgratingstring, "avgstar");
                  dataGridView3.Rows.Add(numresult, avgresult, reviewqresult);
                  dataGridView3.Rows[i].HeaderCell.Value = CategoryListBox.GetItemText(CategoryListBox.CheckedItems[i]);
                  businessnumstring = "select count(distinct business.bid) from business, categories where business.bid = categories.bid and business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and business.city = '" + CityListBox.GetItemText(CityListBox.SelectedItem) + "' and business.zipcode = '" + ZipCodeListBox.GetItemText(ZipCodeListBox.SelectedItem) + "'";
                  reviewsstring = "select avg(business.reviewcount) from business where business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and business.city = '" + CityListBox.GetItemText(CityListBox.SelectedItem) + "' and business.zipcode = '" + ZipCodeListBox.GetItemText(ZipCodeListBox.SelectedItem) + "'  and business.bid in (select categories.bid from categories where";

                  avgratingstring = "select avg(avgstar) from categories, business_average_reviews, business where categories.bid = business_average_reviews.bid and business.bid = categories.bid and business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and business.city = '" + CityListBox.GetItemText(CityListBox.SelectedItem) + "' and business.zipcode = '" + ZipCodeListBox.GetItemText(ZipCodeListBox.SelectedItem) + "' and ";


              }
             
              

          }
          if(CityListBox.SelectedItem != null && StateComboBox.SelectedItem != null) //City has been selected
          {
              string businessnumstring = "select count(distinct business.bid) from business, categories where business.bid = categories.bid and business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and business.city = '" + CityListBox.GetItemText(CityListBox.SelectedItem) + "'";
              string reviewsstring = "select avg(business.reviewcount) from business where business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and business.city = '" + CityListBox.GetItemText(CityListBox.SelectedItem) + "' and business.bid in (select categories.bid from categories where";
              string avgratingstring = "select avg(avgstar) from categories, business_average_reviews, business where categories.bid = business_average_reviews.bid and business.bid = categories.bid and business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and business.city = '" + CityListBox.GetItemText(CityListBox.SelectedItem)  + "' and ";

              
              for( int i =0 ; i < CategoryListBox.CheckedItems.Count;i++)
              {
                  businessnumstring += " and categories.category = '" + CategoryListBox.CheckedItems[i].ToString() + "';";
                  reviewsstring += " categories.category = '" + CategoryListBox.CheckedItems[i].ToString() + "');";
                  avgratingstring += " categories.category = '" + CategoryListBox.CheckedItems[i].ToString() + "';";

                  String numresult = mydb.SQLSELECTExec2(businessnumstring, "bid");
                  String reviewqresult = mydb.SQLSELECTExec2(reviewsstring, "reviewcount");
                  String avgresult = mydb.SQLSELECTExec2(avgratingstring, "avgstar");

                  dataGridView2.Rows.Add(numresult, avgresult, reviewqresult);
                  dataGridView2.Rows[i].HeaderCell.Value = CategoryListBox.GetItemText(CategoryListBox.CheckedItems[i]);
                  businessnumstring = "select count(distinct business.bid) from business, categories where business.bid = categories.bid and business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and business.city = '" + CityListBox.GetItemText(CityListBox.SelectedItem) + "'";

                  avgratingstring = "select avg(avgstar) from categories, business_average_reviews, business where categories.bid = business_average_reviews.bid and business.bid = categories.bid and business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and business.city = '" + CityListBox.GetItemText(CityListBox.SelectedItem) + "' and ";


                  reviewsstring = "select avg(business.reviewcount) from business where business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and business.city = '" + CityListBox.GetItemText(CityListBox.SelectedItem) + "' and business.bid in (select categories.bid from categories where";
              }
          }
          if(StateComboBox.SelectedItem != null)//just the state has been selected.
          {
              string businessnumstring = "select count(distinct business.bid) from business, categories where business.bid = categories.bid and business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "'";


              string avgratingstring = "select avg(avgstar) from categories, business_average_reviews, business where categories.bid = business_average_reviews.bid and business.bid = categories.bid and business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and ";

              
              // string avgratingstring = "select avg(review.stars) from review where review.Bid in (select categories.Bid from categories, business where categories.Bid = business.Bid and business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' ";
              string reviewsstring = "select avg(business.reviewcount) from business where business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and business.bid in (select categories.bid from categories where";
              for (int i = 0; i < CategoryListBox.CheckedItems.Count; i++)
              {
                  businessnumstring += " and categories.category = '" + CategoryListBox.CheckedItems[i].ToString() + "';";
                  reviewsstring += " categories.category = '" + CategoryListBox.CheckedItems[i].ToString() + "');";
                  avgratingstring += " categories.category = '" + CategoryListBox.CheckedItems[i].ToString() + "';";


                  String numresult = mydb.SQLSELECTExec2(businessnumstring, "bid");
                  String reviewqresult = mydb.SQLSELECTExec2(reviewsstring, "reviewcount");
                  String avgresult = mydb.SQLSELECTExec2(avgratingstring, "avgstar");
             


                //  String avgresult = mydb.SQLSELECTExec2(avgratingstring, "stars");
                  dataGridView1.Rows.Add(numresult, avgresult, reviewqresult);
                  dataGridView1.Rows[i].HeaderCell.Value = CategoryListBox.GetItemText(CategoryListBox.CheckedItems[i]);
                  reviewsstring = "select avg(business.reviewcount) from business where business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and business.bid in (select categories.bid from categories where";
                  businessnumstring = "select count(distinct business.bid) from business, categories where business.bid = categories.bid and business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "'";

                  avgratingstring = "select avg(avgstar) from categories, business_average_reviews, business where categories.bid = business_average_reviews.bid and business.bid = categories.bid and business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' and ";
                  
                  
                  //avgratingstring = "select avg(review.stars) from review where review.Bid in (select categories.Bid from categories, business where categories.Bid = business.Bid and business.state = '" + StateComboBox.GetItemText(StateComboBox.SelectedItem) + "' ";

              }

          
          }

      }
        





        /// /////////////////////////////////////////////////////////////START OF SECOND TAB//////////////////////////////////////////////////////////////////
       
      private void StateComboBox2_SelectedIndexChanged(object sender, EventArgs e)
      {
          string qstr = "select distinct city from cities where state = '" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' order by city;";
          string qstr2 = "select sum(population) from cities where state = '" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "';";
          string qstr3 = "select avg(Avg_income) from cities where state = '" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "';";

          CityListBox2.Items.Clear();
          CityListBox2.ResetText();
          ZipCodeListBox2.Items.Clear();
          ZipCodeListBox2.ResetText();
          List<String> qresult = mydb.SQLSELECTExec(qstr, "city");
          String qresult2 = mydb.SQLSELECTExec2(qstr2, "population");
          String qresult3 = mydb.SQLSELECTExec2(qstr3, "Avg_income");
          for (int i = 0; i < qresult.Count(); i++)
          {
              CityListBox2.Items.Add(qresult[i]);
          }
      }

      private void CityListBox2_SelectedIndexChanged(object sender, EventArgs e)
      {
          string qstr = "select distinct zipcode from cities where state = '" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' order by zipcode;";
          string qstr2 = "select sum(population) from cities where state = '" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "';";
          string qstr3 = "select avg(Avg_income) from cities where state = '" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "';";

          ZipCodeListBox2.Items.Clear();
          ZipCodeListBox2.ResetText();
         
          List<String> qresult = mydb.SQLSELECTExec(qstr, "zipcode");
          String qresult2 = mydb.SQLSELECTExec2(qstr2, "population");
          String qresult3 = mydb.SQLSELECTExec2(qstr3, "Avg_income");
          for (int i = 0; i < qresult.Count(); i++)
          {
              ZipCodeListBox2.Items.Add(qresult[i]);
          }
         
      }
      private void CategoryListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
      {
          CheckedListBox items = (CheckedListBox)sender;
          if (items.CheckedItems.Count > 10)
          {
              e.NewValue = CheckState.Unchecked;
          }
      }


      private void SearchButton_Click(object sender, EventArgs e)
      {
          string namestring = "";
          string citystring = "";
          string statestring = "";
          string attributestring = "";
          string zipstring = "";
          string ratingstring = "";
          string reviewcountstring = "";
          string subattstring = "";
          string mainattstring = "";
          string avgratingstring = "";
         
          int deletecount;
          int j = 0;
          dataGridView4.Rows.Clear();
          treeView1.Nodes.Clear();

          treeselectstring = "";
              namestring += "select name from business,  business_average_reviews where ";
              citystring += "select city from business,  business_average_reviews  where";
              statestring += "select state from business,  business_average_reviews  where";
              zipstring += "select zipcode from business,  business_average_reviews  where";
              reviewcountstring += "select reviewcount from business,  business_average_reviews where";
              attributestring += "select distinct att_name from attributes where";
              subattstring += "select distinct att_name from attributes where";
              mainattstring += "select distinct att_name from attributes where";
              treeselectstring += "select distinct att_value from attributes where";
              avgratingstring += "select avgstar from business, business_average_reviews where ";

              if (AttributeListBox.Items.Count != 0)
              {
                  string[] words;
                  string[] words2;
                  string attributes = "";
                  attributes += AttributeListBox.Items[0].ToString();
                  words = attributes.Split('=');


                  namestring += " business.bid in (select bid from attributes where att_name ='" + words[0].ToString() + "' and att_value = '" + words[1].ToString() + "') ";
                  avgratingstring += " business.bid in (select bid from attributes where att_name ='" + words[0].ToString() + "' and att_value = '" + words[1].ToString() + "') ";

                  citystring += " business.bid in (select bid from attributes where att_name ='" + words[0].ToString() + "' and att_value = '" + words[1].ToString() + "') ";
                  statestring += " business.bid in (select bid from attributes where att_name ='" + words[0].ToString() + "' and att_value = '" + words[1].ToString() + "') ";
                  zipstring += " business.bid in (select bid from attributes where att_name ='" + words[0].ToString() + "' and att_value = '" + words[1].ToString() + "') ";
                  ratingstring += " business.bid in (select bid from attributes where att_name ='" + words[0].ToString() + "' and att_value = '" + words[1].ToString() + "') ";
                  reviewcountstring += " business.bid in (select bid from attributes where att_name ='" + words[0].ToString() + "' and att_value = '" + words[1].ToString() + "') ";
                  attributes = "";
                  for (int i = 1; i < AttributeListBox.Items.Count; i++)
                  {
                      attributes += AttributeListBox.Items[i].ToString();
                      words2 = attributes.Split('=');
                      namestring += "and business.bid in( select bid from attributes where att_name = '" + words2[0].ToString() + "' and att_value = '" + words2[1].ToString() + "')";
                      avgratingstring +=  "and business.bid in( select bid from attributes where att_name = '" + words2[0].ToString() + "' and att_value = '" + words2[1].ToString() + "')";

                      citystring += " and business.bid in( select bid from attributes where att_name = '" + words2[0].ToString() + "' and att_value = '" + words2[1].ToString() + "')";
                      statestring += " and business.bid in( select bid from attributes where att_name = '" + words2[0].ToString() + "' and att_value = '" + words2[1].ToString() + "')";
                      zipstring += " and business.bid in( select bid from attributes where att_name = '" + words2[0].ToString() + "' and att_value = '" + words2[1].ToString() + "')";
                      ratingstring += " and business.bid in( select bid from attributes where att_name = '" + words2[0].ToString() + "' and att_value = '" + words2[1].ToString() + "')";
                      reviewcountstring += " and business.bid in( select bid from attributes where att_name = '" + words2[0].ToString() + "' and att_value = '" + words2[1].ToString() + "')";
                      attributes = "";
                      
                  }
                  namestring += " and";
                  avgratingstring += " and";
                  citystring += " and";
                  statestring += " and";
                  zipstring += " and";
                  ratingstring += " and";
                  reviewcountstring += " and";
                   
              }
          if(ZipCodeListBox2.SelectedItem != null )// everything has been chosen
            {
                namestring += " business.bid = business_average_reviews.bid and business.state ='"
                           + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' " + "and business.zipcode = '" + ZipCodeListBox2.GetItemText(ZipCodeListBox2.SelectedItem) + "' ";

                avgratingstring += " business.bid = business_average_reviews.bid and business.state ='"
                             + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' " + "and business.zipcode = '" + ZipCodeListBox2.GetItemText(ZipCodeListBox2.SelectedItem) + "' ";


                citystring += " business.bid = business_average_reviews.bid and business.state ='"
                        + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' " + " and business.zipcode = '" + ZipCodeListBox2.GetItemText(ZipCodeListBox2.SelectedItem) + "' ";

                statestring += " business.bid = business_average_reviews.bid and business.state = '" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' and business.zipcode = '" + ZipCodeListBox2.GetItemText(ZipCodeListBox2.SelectedItem) + "' ";

                zipstring += " business.bid = business_average_reviews.bid and business.state = '" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' and business.zipcode = '" + ZipCodeListBox2.GetItemText(ZipCodeListBox2.SelectedItem) + "' ";

                reviewcountstring += " business.bid = business_average_reviews.bid and business.state = '" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' and business.zipcode = '" + ZipCodeListBox2.GetItemText(ZipCodeListBox2.SelectedItem) + "' ";

               // ratingstring += "select avg(review.stars) from business, categories, review where business.bid = categories.bid and business.bid = review.bid and business.state = '" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' and business.zipcode = '" + ZipCodeListBox2.GetItemText(ZipCodeListBox2.SelectedItem) + "' ";

                attributestring += " attributes.bid in (select business.bid from business, business_average_reviews where business.state ='" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem)
                           + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' and business.zipcode = '" + ZipCodeListBox2.GetItemText(ZipCodeListBox2.SelectedItem) + "' and business.bid = business_average_reviews.bid ";


                subattstring += " attributes.bid in (select business.bid from business, business_average_reviews where business.state ='" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem)
                      + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' and business.zipcode = '"
                        + ZipCodeListBox2.GetItemText(ZipCodeListBox2.SelectedItem) + "' and business.bid = business_average_reviews.bid ";

                mainattstring += " attributes.bid in (select business.bid from business, business_average_reviews where business.state ='" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem)
                        + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' and business.zipcode = '"
                          + ZipCodeListBox2.GetItemText(ZipCodeListBox2.SelectedItem) + "' and business.bid = business_average_reviews.bid ";
                treeselectstring += " attributes.bid in (select business.bid from business, business_average_reviews where business.state ='" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem)
                        + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' and business.zipcode = '"
                          + ZipCodeListBox2.GetItemText(ZipCodeListBox2.SelectedItem) + "' and business.bid = business_average_reviews.bid ";
          }
          else if (StateComboBox2.SelectedItem != null && CityListBox2.SelectedItem != null) //user chose state and city
            {

                namestring += " business.bid = business_average_reviews.bid and business.state ='"
                           + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' ";
                avgratingstring += " business.bid = business_average_reviews.bid and business.state ='"
                             + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' ";

                citystring += " business.bid = business_average_reviews.bid and business.state ='"
                            + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' ";
                statestring += " business.bid = business_average_reviews.bid and business.state ='"
                                  + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' ";
                zipstring += " business.bid = business_average_reviews.bid and business.state ='"
                                     + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' ";
                reviewcountstring += " business.bid = business_average_reviews.bid and business.state ='"
                                    + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' ";
                 //  ratingstring += "select avg(rating.stars) from business, categories, review where business.bid = categories.bid and business.state ='"
                           //         + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' "; 

                attributestring += " attributes.bid in (select business.bid from business, business_average_reviews where business.state ='" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem)
                           + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' and business.bid = business_average_reviews.bid";

                   subattstring += " attributes.bid in (select business.bid from business, business_average_reviews where business.state ='" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem)
                       + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' and business.bid = business_average_reviews.bid";

                   mainattstring += " attributes.bid in (select business.bid from business, business_average_reviews where business.state ='" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem)
                        + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' and business.zipcode = '"
                          + ZipCodeListBox2.GetItemText(ZipCodeListBox2.SelectedItem) + "' and business.bid = business_average_reviews.bid";
              treeselectstring += " attributes.bid in (select business.bid from business, business_average_reviews where business.state ='" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem)
                        + "' and business.city = '" + CityListBox2.GetItemText(CityListBox2.SelectedItem) + "' and business.bid = business_average_reviews.bid";
          }
         /* else if(AttributeListBox.Items.Count != 0) //there are more specific attributes
          {
             string newattstring = "";
              for(int i =0; i < AttributeListBox.Items.Count;i++)
              {

              }

          }*/
          else //just state is chosen
          {
              namestring += " business.bid = business_average_reviews.bid and business.state ='"
                           + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' ";

              avgratingstring += " business.bid = business_average_reviews.bid and business.state ='"
                           + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' ";

              citystring += " business.bid = business_average_reviews.bid and business.state ='"
                             + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' ";
              statestring += " business.bid = business_average_reviews.bid and business.state ='"
                             + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' ";
              zipstring += " business.bid = business_average_reviews.bid and business.state ='"
                             + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' ";
              reviewcountstring += " business.bid = business_average_reviews.bid and business.state ='"
                             + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' ";


              attributestring += " attributes.bid in (select business.bid from business, business_average_reviews where business.state ='" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem)
                        + "' and business.bid = business_average_reviews.bid";

              subattstring += " attributes.bid in (select business.bid from business, business_average_reviews where business.state = '" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.bid = business_average_reviews.bid";

              mainattstring += " attributes.bid in (select business.bid from business, business_average_reviews where business.state = '" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.bid = business_average_reviews.bid";

              treeselectstring += " attributes.bid in (select business.bid from business, business_average_reviews where business.state = '" + StateComboBox2.GetItemText(StateComboBox2.SelectedItem) + "' and business.bid = business_average_reviews.bid";

          }
          for (int i = 0; i < CategoryListBox2.CheckedItems.Count; i++) //searches for businesses of each category
          {
              namestring += " and business.bid in (select bid from categories where category = '" + CategoryListBox2.CheckedItems[i].ToString() + "') ";
              avgratingstring += " and business.bid in (select bid from categories where category = '" + CategoryListBox2.CheckedItems[i].ToString() + "') ";
              citystring += " and business.bid in (select bid from categories where category = '" + CategoryListBox2.CheckedItems[i].ToString() + "') ";
              attributestring += " and business.bid in (select bid from categories where category = '" + CategoryListBox2.CheckedItems[i].ToString() + "') ";
              statestring += " and business.bid in (select bid from categories where category = '" + CategoryListBox2.CheckedItems[i].ToString() + "') ";
              zipstring += " and business.bid in (select bid from categories where category = '" + CategoryListBox2.CheckedItems[i].ToString() + "') ";
              reviewcountstring += " and business.bid in (select bid from categories where category = '" + CategoryListBox2.CheckedItems[i].ToString() + "') ";
              subattstring += " and business.bid in (select bid from categories where category = '" + CategoryListBox2.CheckedItems[i].ToString() + "') ";
              mainattstring += " and business.bid in (select bid from categories where category = '" + CategoryListBox2.CheckedItems[i].ToString() + "') ";
              treeselectstring += " and business.bid in (select bid from categories where category = '" + CategoryListBox2.CheckedItems[i].ToString() + "') ";
              //ratingstring += " and categories.category = '" + CategoryListBox2.CheckedItems[i].ToString() + "' ";


          }


          //////////////////////////////Every Filter Selected////////////////////////////////////////////
          if (MinReviewTextBox.Text != "" && MaxReviewTextBox.Text != "" && MinRatingComboBox.SelectedItem != null && MaxRatingComboBox.SelectedItem != null) // all filters have been selected.
          {
              attributestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "') and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
              mainattstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "') and attributes.att_value ='TABLE';";
              subattstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "') and attributes.parent = '";
              namestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
              citystring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
              statestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
              zipstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
              ratingstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
              reviewcountstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
              avgratingstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";

              treeselectstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "')";
          }





              ///////////////////////////////Both Filters selected Cases/////////////////////////////////////////////////////

          else if(MinRatingComboBox.SelectedItem != null && MaxRatingComboBox.SelectedItem != null) // if just both rating filters have been selected
          {
              attributestring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              mainattstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              subattstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              namestring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              citystring +=  " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              statestring +=  " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              zipstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              ratingstring +=  " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              reviewcountstring +=  " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              avgratingstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
                treeselectstring +=  " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
         
          if(MinReviewTextBox.Text != "")
          {
              attributestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "') and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
              mainattstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "') and attributes.att_value ='TABLE';";
              subattstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "') and attributes.parent = '";
              namestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
              citystring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
              statestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
              zipstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
              ratingstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
              reviewcountstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
              treeselectstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "')";
              avgratingstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
          }
          else if(MaxReviewTextBox.Text != "")
          {
              attributestring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "') and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
              mainattstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "') and attributes.att_value ='TABLE';";
              subattstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "') and attributes.parent = '";
              namestring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
              citystring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
              statestring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
              zipstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
              ratingstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
              reviewcountstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
              treeselectstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "')";
              avgratingstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
          }
          else
          {
              attributestring += ") and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
              mainattstring += ") and attributes.att_value ='TABLE';";
              subattstring += ") and attributes.parent = '";
              namestring += ";";
              avgratingstring += ";";
              citystring += ";";
              statestring += ";";
              zipstring += ";";
              ratingstring += ";";
              reviewcountstring += ";";
              treeselectstring += ")";
          }
          
          }
          




          else if (MinReviewTextBox.Text != "" && MaxReviewTextBox.Text != "") // both review count filters are selected.
          {
              attributestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' ";
              mainattstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "'  ";
              subattstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' ";
              namestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' ";
              citystring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' ";
              statestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' ";
              zipstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' ";
              ratingstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' ";
              reviewcountstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' ";
              treeselectstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' ";
              avgratingstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business.reviewcount < '" + MaxReviewTextBox.Text + "' ";

              if(MinRatingComboBox.SelectedItem != null)
              {
                  attributestring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "') and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
                  mainattstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "')  and attributes.att_value ='TABLE';";
                  subattstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "') and attributes.parent = '";
                  namestring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
                  citystring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
                  statestring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
                  zipstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
                  ratingstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
                  reviewcountstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
                  treeselectstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "')";
                  avgratingstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
              }
              else if(MaxRatingComboBox.SelectedItem != null)
              {
                  attributestring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "') and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
                  mainattstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "')  and attributes.att_value ='TABLE';";
                  subattstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "') and attributes.parent = '";
                  namestring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
                  citystring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
                  statestring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
                  zipstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
                  ratingstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
                  reviewcountstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
                  treeselectstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "')";
                  avgratingstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
              }
              else
              {
                  attributestring += ") and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
                  mainattstring += ") and attributes.att_value ='TABLE';";
                  subattstring += ") and attributes.parent = '";
                  namestring += ";";
                  avgratingstring += ";";
                  citystring += ";";
                  statestring += ";";
                  zipstring += ";";
                  ratingstring += ";";
                  reviewcountstring += ";";
                  treeselectstring += ")";
              }


          }

              //////////////////////////////////////InBetween Cases///////////////////////////////////////////////////////


          else if(MinRatingComboBox.SelectedItem != null && MinReviewTextBox.Text != "") // both mins are selected
          {
              attributestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' "; 
              mainattstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              subattstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              namestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              citystring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              statestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              zipstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              ratingstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              reviewcountstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              treeselectstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";
              avgratingstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "' and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";



              if(MaxRatingComboBox.SelectedItem != null) //if the max rating is also selected
              {
                  attributestring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "') and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
                  mainattstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "')  and attributes.att_value ='TABLE';";
                  subattstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "') and attributes.parent = '";
                  namestring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
                  citystring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
                  statestring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
                  zipstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
                  ratingstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
                  reviewcountstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
                  treeselectstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "')";
                  avgratingstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";

              }
              else if( MaxReviewTextBox.Text != "")
              {
                  attributestring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "') and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
                  mainattstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "') and attributes.att_value ='TABLE';";
                  subattstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "') and attributes.parent = '";
                  namestring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
                  citystring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
                  statestring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
                  zipstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
                  ratingstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
                  reviewcountstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
                  treeselectstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "')";
                  avgratingstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";

              }
              else
              {
                  attributestring += ") and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
                  mainattstring += ") and attributes.att_value ='TABLE';";
                  subattstring += ") and attributes.parent = '";
                  namestring += ";";
                  avgratingstring += ";";
                  citystring += ";";
                  statestring += ";";
                  zipstring += ";";
                  ratingstring += ";";
                  reviewcountstring += ";";
                  treeselectstring += ")";
              }
          }

          else if(MaxRatingComboBox.SelectedItem != null && MaxReviewTextBox.Text != "") // if both max filters are selected
          {
              attributestring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem)+ "' ";
              mainattstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem)+ "' ";
              subattstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem)+ "' ";
              namestring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem)+ "' ";
              citystring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem)+ "' ";
              statestring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem)+ "' ";
              zipstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem)+ "' ";
              ratingstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem)+ "' ";
              reviewcountstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem)+ "' ";
              treeselectstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem)+ "' ";
              avgratingstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "' and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "' ";


              if(MinRatingComboBox.SelectedItem != null) //if the min rating is also selected
              {
                  attributestring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "') and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
                  mainattstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "')  and attributes.att_value ='TABLE';";
                  subattstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "') and attributes.parent = '";
                  namestring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
                  citystring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
                  statestring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
                  zipstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
                  ratingstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
                  reviewcountstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
                  treeselectstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "')";
                  avgratingstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";

              }
              else if(MinReviewTextBox.Text != "")
              {
                  attributestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "') and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
                  mainattstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "') and attributes.att_value ='TABLE';";
                  subattstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "') and attributes.parent = '";
                  namestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
                  citystring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
                  statestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
                  zipstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
                  ratingstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
                  reviewcountstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
                  treeselectstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "')";
                  avgratingstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";

              }
              else
              {
                  attributestring += ") and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
                  mainattstring += ") and attributes.att_value ='TABLE';";
                  subattstring += ") and attributes.parent = '";
                  namestring += ";";
                  avgratingstring += ";";

                  citystring += ";";
                  statestring += ";";
                  zipstring += ";";
                  ratingstring += ";";
                  reviewcountstring += ";";
                  treeselectstring += ")";
              }
          }



              ///////////////////////////only one filter cases////////////////////////////////////////////////////////

          else if(MinReviewTextBox.Text != "") //only the minreviews box has been selected
          {
              attributestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "') and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
              mainattstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "') and attributes.att_value ='TABLE';";
              subattstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "') and attributes.parent = '";
              namestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
              citystring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
              statestring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
              zipstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
              ratingstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
              reviewcountstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";
              treeselectstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "')";
              avgratingstring += " and business.reviewcount > '" + MinReviewTextBox.Text + "';";

          }


          else if(MinRatingComboBox.SelectedItem != null) // just the min rating box has been selected
          {
              attributestring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "') and (attributes.att_value = 'TABLE' or attributes.parent) = '' order by att_name;";
              mainattstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "')  and attributes.att_value ='TABLE';";
              subattstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "') and attributes.parent = '";
              namestring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
              citystring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
              statestring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
              zipstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
              ratingstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
              reviewcountstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";
              treeselectstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "')";
              avgratingstring += " and business_average_reviews.avgstar > '" + MinRatingComboBox.GetItemText(MinRatingComboBox.SelectedItem) + "';";

          }


          else if (MaxRatingComboBox.SelectedItem != null) // just the max rating box has been selected
          {
              attributestring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "') and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
              mainattstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "')  and attributes.att_value ='TABLE';";
              subattstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "') and attributes.parent = '";
              namestring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
              citystring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
              statestring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
              zipstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
              ratingstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
              reviewcountstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";
              treeselectstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "')";
              avgratingstring += " and business_average_reviews.avgstar < '" + MaxRatingComboBox.GetItemText(MaxRatingComboBox.SelectedItem) + "';";

          }

          else if(MaxReviewTextBox.Text != "") //only the maxreviews box is selected.
          {
              attributestring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "') and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
              mainattstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "') and attributes.att_value ='TABLE';";
              subattstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "') and attributes.parent = '";
              namestring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
              citystring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
              statestring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
              zipstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
              ratingstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
              reviewcountstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";
              treeselectstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "')";
              avgratingstring += " and business.reviewcount < '" + MaxReviewTextBox.Text + "';";

          }


          else//no additonal filters have been selected and attribute list box is empty.
          {


              attributestring += ") and (attributes.att_value = 'TABLE' or attributes.parent = '') order by att_name;";
              mainattstring += ") and attributes.att_value ='TABLE';";
              subattstring += ") and attributes.parent = '";
              namestring += ";";
              avgratingstring += ";";
              citystring += ";";
              statestring += ";";
              zipstring += ";";
              ratingstring += ";";
              reviewcountstring += ";";
              treeselectstring += ")";
          }
         
          
          
          List<String> qresult = mydb.SQLSELECTExec(namestring, "name");
          List<String> qresult2 = mydb.SQLSELECTExec(citystring, "city");
          List<String> qresult3 = mydb.SQLSELECTExec(statestring, "state");
          List<String> qresult4 = mydb.SQLSELECTExec(zipstring, "zipcode");
          List<String> qresult5 = mydb.SQLSELECTExec(reviewcountstring, "reviewcount");
          List<String> mainattresult = mydb.SQLSELECTExec(mainattstring, "att_name");
          List<String> avgstarresult = mydb.SQLSELECTExec(avgratingstring, "avgstar");
          //String qresult6 = "";
          List<String> subattresult;
        List<String> attresult = mydb.SQLSELECTExec(attributestring, "att_name");
        
          for(int i =0; i < qresult.Count(); i++)
          {
            //  qresult6 = mydb.SQLSELECTExec2(ratingstring, "stars");
              dataGridView4.Rows.Add(qresult[i], qresult2[i], qresult3[i], qresult4[i], avgstarresult[i], qresult5[i]);
             // qresult6 = "";
          }
          if (qresult.Count() != 0) //there is atleast one business found in the search.
          {


              for (int i = 0; i < attresult.Count(); i++)
              {

                  treeView1.Nodes.Add(attresult[i]);
                  if (mainattresult.Count() != 0) //there are some main attributes that have subattributes.
                  {
                      if (attresult[i].Contains(mainattresult[j])) // only if there are subattributes.
                      {

                          deletecount = 0;
                          deletecount = mainattresult[j].Length + 2;
                          subattstring += mainattresult[j] + "';";
                          subattresult = mydb.SQLSELECTExec(subattstring, "att_name");
                          for (int k = 0; k < subattresult.Count(); k++)
                          {
                              treeView1.Nodes[i].Nodes.Add(subattresult[k]);
                          }
                          subattstring = subattstring.Remove(subattstring.Length - deletecount);
                          if (j < mainattresult.Count() - 1)
                              j++;
                      }
                  }
              }
          }

          
      }

      private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
      {
          ValueBox.Items.Clear();
          string temp = "";
          temp = treeselectstring;
          //String attvaluestring = "select distinct att_value from attributes where att_name = '" + treeView1.SelectedNode.Text + "';";
          temp += " and att_name = '" + treeView1.SelectedNode.Text + "';";
          List<String> attvalueresult = mydb.SQLSELECTExec(temp, "att_value");
          for(int i =0; i < attvalueresult.Count(); i++)
          {
              ValueBox.Items.Add(attvalueresult[i]);
          }

      }

     

      private void AttributeAddButton_Click(object sender, EventArgs e)
      {
         
          string selectedatt = "";
              selectedatt += treeView1.SelectedNode.Text + "=" + ValueBox.GetItemText(ValueBox.SelectedItem);
          
              
          AttributeListBox.Items.Add(selectedatt);
      }

      private void RemoveButton_Click(object sender, EventArgs e)
      {
          AttributeListBox.Items.Remove(AttributeListBox.SelectedItem);
      }

      private void ShowBusinessesButton_Click(object sender, EventArgs e)
      {
          List<String> CategoriesfromForm1 = new List<String>();
          for(int i =0; i < CategoryListBox2.CheckedItems.Count; i++)
          {
              CategoriesfromForm1.Add(CategoryListBox2.CheckedItems[i].ToString());
          }
          Form2 F2 = new Form2(passedbid, CategoriesfromForm1);
          F2.Show();
      }

      private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
      {
          
         
          string bidstring = "select bid from business where name = '" + dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString() + "' and business.city = '" + dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString()
          +"' and business.state = '" + dataGridView4.Rows[e.RowIndex].Cells[2].Value.ToString() + "';";
          passedbid = mydb.SQLSELECTExec(bidstring, "bid");
      }

     

    




      

       

       

       

    }
}
