<?php
header("Access-Control-Allow-Credentials: true");
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Access-Control-Allow-Headers: Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time');

	$con = mysqli_connect('localhost', 'u635035423_razi', 'PpSp@Wz.BDDVG8j', 'u635035423_Digipen');

	//check that connection happened
	if(mysqli_connect_errno())
	{
		echo "1: Connection failed"; //error code #1 = connection failed
		exit();
	}
	
	$email = $_POST["email"];
	$tabledataquery = "SELECT * FROM `networkingTest` WHERE email = '" . $email . "'";
	
	
	if($result = mysqli_query($con,$tabledataquery))
	{
		while($row = mysqli_fetch_row($result))
		{
			echo $row[0] . "|" . $row[3];
		}
	}
	else
		echo "";
?>
