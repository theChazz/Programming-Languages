<?php
//connection to database
include("includes/db_connection.php");
$obj = new db_connection();

if(isset($_POST['delete']))
{
		//mysqli_real_escape_string helps prevent SQL injection from form 
		foreach($_POST['delete'] as $id)
			{
				$delete	= mysqli_real_escape_string($db_connection,$id);
			}            
		//delete one product at a time
		foreach($_POST['delete'] as $id)
			{
				$delete_query = $obj->delete_selected_products($id);
			}
		//if delete of product is successful, redirect back to product list page
		header ("location: product_list_page.php?success");
		exit();
}
else 
{
	//if delete of product is not successful, redirect back to product list page with error msg
	$_SESSION['error_array'] = 'No item, was selected to delete.';
	header ("location: product_list_page.php?failed");
	exit();
}

?>


