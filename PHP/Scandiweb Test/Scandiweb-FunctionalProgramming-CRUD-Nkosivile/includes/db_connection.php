<?php

include("db_config.php");

date_default_timezone_set("Africa/Johannesburg");
$db_connection = mysqli_connect(server,username,password,database) or die("connection was not established");


//All CRUD functions of database
class db_connection
{
	private $db_connection;

	function __construct()
	{
		$this->db_connection = mysqli_connect(server,username,password,database) or die("connection was not established");
	}
	
	// FETCH ALL PRODUCTS
	function fetch_all_poducts()
	{
		$sql = " SELECT * FROM `products` ";
		$query = mysqli_query($this->db_connection,$sql) or die(mysqli_error($this->db_connection));
		if(mysqli_num_rows($query)!=0)
		{
			return $query;
		}
		else
		{
			return false;
		}
	}
	
	// DELETE SELECTED PRODUCTS
	function delete_selected_products($id)
	{
		//$sql = " DELETE FROM `products` WHERE id=`$id` ";
		$sql = " DELETE FROM `products` WHERE id = $id ";
		$query = mysqli_query($this->db_connection,$sql) or die(mysqli_error($this->db_connection));
		if(mysqli_num_rows($query)!=0)
		{
			return $query;
		}
		else
		{
			return false;
		}
	}
	
	// ADD PRODUCT
	function insert_new_product($SKU, $name, $price, $size, $type)
	{
		//Insert or register new user in database
        $sql = "INSERT INTO `products` (`sku`, `name`, `price`, `attribute`, `product_type`) VALUES ('$SKU', '$name', '$price', '$size', '$type');";
		
        $query = mysqli_query($this->db_connection, $sql) or die(mysqli_error());
        if($query)
        {
        	return $query;
        }
        else
        {
        	return false;
        }
	}

}

?>

