using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Security;
using System.Collections;
using System.Collections.Generic;

public class RegisterAccount : MonoBehaviour {

	private string SecretKey = "123456";
	private string RegisterPHP_Url = "https://immersivemedia.ph/otsukadb/ot_register.php";
	private InputField Input_tblUser_FName = null;
	private InputField Input_tblUser_LName = null;
	private InputField Input_tblUser_Age = null;
	private  Dropdown Input_tblUser_Gender = null;
	private Dropdown Input_tblUser_Location = null;
	List<string> m_DropOptions = new List<string> { 
		"Location",
		"Alaminos", "Angeles",
		"Antipolo", 

		"Bacolod ", "Bacoor ", "Bago ", "Baguio ", 
		"Bais ", "Balanga ", "Batac ", "Batangas  City", "Bayawan ", 
		"Baybay ", "Bayugan ", "Biñan ", "Bislig ", "Bogo ",
		"Borongan ","Butuan ",

		"Cabadbaran ","Cabanatuan ","Cabuyao ",
		"Cadiz ","Cagayan de Oro ","Calamba ","Calapan ","Calbayog ",
		"Caloocan ","Candon ","Canlaon ","Carcar ","Catbalogan ",
		"Cauayan ","Cavite City","Cebu City","Cotabato City",

		"Dagupan","Danao","Dapitan",
		"Dasmariñas","Davao City","Digos","Dipolog","Dumaguete",

		"El Salvador","Escalante",

		"Gapan","General Santos","General Trias","Gingoog","Guihulngan",

		"Himamaylan",

		"Ilagan","Iligan","Iloilo City","Imus","Iriga","Isabela",

		"Kabankalan","Kidapawan","Koronadal","La Carlota",

		"Lamitan","Laoag","Lapu-Lapu","Las Piñas","Legazpi",
		"Ligao","Lipa","Lucena",

		"Maasin","Mabalacat","Makati","Malabon","Malaybalay",
		"Malolos","Mandaluyong","Mandaue","Manila","Marawi",
		"Marikina","Masbate City","Mati","Meycauayan","Muñoz",
		"Muntinlupa",

		"Naga","Navotas",

		"Olongapo","Ormoc","Oroquieta","Ozamiz",

		"Pagadian","Palayan","Panabo","Parañaque",
		"Pasay","Pasig","Passi","Puerto Princesa",

		"Quezon City",

		"Roxas",

		"Sagay","Samal","San Carlos","San Carlos","San Fernando",
		"San Fernando","San Jose","San Jose del Monte","San Juan",
		"San Pablo","San Pedro","Santa Rosa","Santiago","Silay",
		"Sipalay","Sorsogon City","Surigao City",

		"Tabaco","Tabuk","Tacloban","Tacurong","Tagaytay",
		"Tagbilaran","Taguig","Tagum","Talisay","Tanauan",
		"Tandag","Tangub","Tanjay","Tarlac City","Tayabas",
		"Toledo","Trece Martires","Tuguegarao",

		"Urdaneta",

		"Valencia","Valenzuela","Victorias","Vigan",

		"Zamboanga City"
	};

	protected string tblUser_Device = "";
	protected string tblUser_FName = "";  
	protected string tblUser_LName = "";
	protected string tblUser_Age = "";
	protected string tblUser_Gender = "";
	protected string tblUser_Location = "";
	private bool isRegister = false;
	protected Text ErroMessage = null;
	private GameObject LoadingPage;

	public AudioSource ClickMusic;

	// Use this for initialization
	void Start () {
		ClickMusic = GameObject.Find("ClickMusic").GetComponent<AudioSource>();

		tblUser_Device = SystemInfo.deviceUniqueIdentifier;

		Input_tblUser_FName = GameObject.Find ("tblUser_FName").GetComponent<InputField> ();
		Input_tblUser_LName = GameObject.Find ("tblUser_LName").GetComponent<InputField> ();
		Input_tblUser_Age = GameObject.Find ("tblUser_Age").GetComponent<InputField> ();
		Input_tblUser_Gender = GameObject.Find ("tblUser_Gender").GetComponent<Dropdown> ();
		Input_tblUser_Location = GameObject.Find ("tblUser_Location").GetComponent<Dropdown> ();

		//Clear the old options of the Dropdown menu
		Input_tblUser_Location.ClearOptions();
		//Add the options created in the List above
		Input_tblUser_Location.AddOptions(m_DropOptions);

		ErroMessage = GameObject.Find ("ErroMessage").GetComponent<Text> ();
		ErroMessage.text = "";

		LoadingPage = GameObject.Find ("LoadingPage");
		LoadingPage.SetActive (false);

		//PlayerPrefs.DeleteAll();

		string isRegister = PlayerPrefs.GetString ("isRegister");
		if(isRegister == "YES"){
			Application.LoadLevel("Scene_02_Game");
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input_tblUser_FName != null)
		{
			tblUser_FName = Input_tblUser_FName.text;
		}
		if (Input_tblUser_LName != null)
		{
			tblUser_LName = Input_tblUser_LName.text;
		}
		if (Input_tblUser_Age != null)
		{
			tblUser_Age = Input_tblUser_Age.text;
		}
//		if (Input_tblUser_Gender.options [Input_tblUser_Gender.value].text == "Gender")
//		{
//			
//		}

//		if (Input_tblUser_Location != null)
//		{
//			tblUser_Location = Input_tblUser_Location.text;
//		}

		tblUser_Location = Input_tblUser_Location.options [Input_tblUser_Location.value].text;
		tblUser_Gender = Input_tblUser_Gender.options [Input_tblUser_Gender.value].text;
		//print ("tblUser_Gender: "+tblUser_Gender);
	}

	/// <summary>
	/// 
	/// </summary>
	public void Register()
	{
		if (isRegister)
			return;

		if (tblUser_FName != string.Empty && tblUser_LName != string.Empty && tblUser_Age != string.Empty && tblUser_Gender != string.Empty && tblUser_Location != string.Empty &&  tblUser_Gender != "Gender" && tblUser_Location != "Location") {
			bool isInputOk = true;
			if (Input_tblUser_FName.text.Contains ("Last") || Input_tblUser_FName.text.Contains ("First") || Input_tblUser_FName.text.Contains ("Name")) {
				isInputOk = false;
			}
			if(Input_tblUser_LName.text.Contains ("Last") || Input_tblUser_LName.text.Contains ("First") || Input_tblUser_LName.text.Contains ("Name")){
				isInputOk = false;
			}

			if (isInputOk == true) {
				StartCoroutine (RegisterProcess ());
			} else {
				string Info = "Invalid Full Name or Hospital Name";
				StartCoroutine (ErrorProcess (Info));
			}

		} else {
			print ("Please fill out all required entry fields");
			string Info = "Please fill out all required entry fields";
			StartCoroutine (ErrorProcess (Info));
		}
		ClickMusic.Play(0);
	}

	public void SkipData()
	{
		if (isRegister)
			return;

		if (tblUser_FName != string.Empty && tblUser_LName != string.Empty)
		{
			bool isInputOk = true;
			if(Input_tblUser_FName.text.Contains ("Last") || Input_tblUser_FName.text.Contains ("First") || Input_tblUser_FName.text.Contains ("Name")){
				isInputOk = false;
			}
			if(Input_tblUser_LName.text.Contains ("Last") || Input_tblUser_LName.text.Contains ("First") || Input_tblUser_LName.text.Contains ("Name")){
				isInputOk = false;
			}


			if (isInputOk == true) {
				StartCoroutine (RegisterProcess ());
			} else {
				string Info = "Invalid Full Name or Hospital Name";
				StartCoroutine (ErrorProcess (Info));
			}

		}else {
			print ("Please fill up the Doctor's Full Name");
			string Info = "Required Doctor's Full Name And Hospital Name";
			StartCoroutine (ErrorProcess (Info));
		}
		ClickMusic.Play(0);
	}
	IEnumerator ErrorProcess(string Info)
	{
		ErroMessage.text = Info;
		yield return new WaitForSeconds(2.5f);
		ErroMessage.text = "";
	}


	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	IEnumerator RegisterProcess()
	{
		if (isRegister)
			yield return null;

		isRegister = true;
		//Used for security check for authorization to modify database
		//$real_hash = md5($tblUser_Device . $tblUser_FName . $tblUser_LName);
		if(tblUser_Age == ""){
			tblUser_Age = "None";
		}
		if(tblUser_Gender == "Gender"){
			tblUser_Gender = "None";
		}
		if(tblUser_Location == ""){
			tblUser_Location = "None";
		}
		if(tblUser_Location == "Location"){
			tblUser_Location = "None";
		}
		if(tblUser_LName == ""){
			tblUser_LName = "None";
		}
		LoadingPage.SetActive (true);
		string hash = Md5Sum(tblUser_Device + tblUser_FName + tblUser_LName).ToLower();



		//Assigns the data we want to save
		//Where -> Form.AddField("name" = matching name of value in SQL database
		WWWForm mForm = new WWWForm();
		mForm.AddField("tblUser_Device", tblUser_Device); // adds the player name to the form
		mForm.AddField("tblUser_FName", tblUser_FName); // adds the player password to the form
		mForm.AddField("tblUser_LName", tblUser_LName); // adds the kill total to the form
		mForm.AddField("tblUser_Age", tblUser_Age); // adds the death Total to the form
		mForm.AddField("tblUser_Gender", tblUser_Gender); // adds the score Total to the form
		mForm.AddField("tblUser_Location", tblUser_Location); // adds the score Total to the form
		mForm.AddField("hash", hash); // adds the security hash for Authorization

		//Creates instance of WWW to runs the PHP script to save data to mySQL database
		WWW www = new WWW(RegisterPHP_Url, mForm);
		Debug.Log("Processing...");
		yield return www;

		Debug.Log("" + www.text);
		if (www.text == "Done")
		{
			Debug.Log("Registered Successfully.");
			PlayerPrefs.SetString ("isRegister", "YES");
			Application.LoadLevel("Scene_01_Tutorial");

		}
		else
		{
			PlayerPrefs.SetString ("isRegister", "NO");
			string Info = "Connection Error";
			StartCoroutine (ErrorProcess (Info));
			Debug.Log(www.text);
			LoadingPage.SetActive (false);
		}
		isRegister = false;
	}

	public string Md5Sum(string input)
	{
		System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
		byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
		byte[] hash = md5.ComputeHash(inputBytes);

		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < hash.Length; i++) { sb.Append(hash[i].ToString("X2")); }
		return sb.ToString();
	}
}
