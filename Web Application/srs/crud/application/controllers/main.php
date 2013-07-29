<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Main extends CI_Controller {

	function __construct()
	{
		session_start();
		parent::__construct();
		
		
		$this->load->library('session');
		
		$newdata = array(
                   'username'  => $_SESSION['username'],
                   'userrole'     => $_SESSION['userrole'],
                   'logged_in' => TRUE
               );

		$this->session->set_userdata($newdata);
		
		//IF not system admin or web admin
		if($this->session->userdata('userrole')!=3 && $this->session->userdata('userrole')!=1) {
			//redirect('srs/main.php');
			$_SESSION = array();
			session_destroy();
			 header( 'Location: /../srs/main.php' ) ;
		}
		else{

		
		$this->load->database();
		$this->load->helper('url');
		
		$this->load->library('grocery_CRUD');	}
	}
	
	function _example_output($output = null)
	{
		$this->load->view('srs_template.php',$output);	
	}
	
	
	function index()
	{
		$this->_example_output((object)array('output' => '' , 'js_files' => array() , 'css_files' => array()));
	}	
	
	
	/*********************************
	********  User Management  *******
	*********************************/
	function user_management($operation = null)
	{
		try{
			/* This is only for the autocompletion */
			$crud = new grocery_CRUD();

			$crud->set_theme('datatables');
			$crud->set_table('users');
			$crud->set_subject('User Record');
			
			

			$crud->columns('username','password','user_role');
			$crud->display_as('username','Username')
				 ->display_as('password','Password')
				 ->display_as('user_role','User Role');
			
			/*$crud->callback_add_field('mobile_user',array($this,'add_field_mobile_user'));
			$crud->callback_edit_field('mobile_user',array($this,'edit_field_mobile_user'));*/
			
			$crud->set_relation('user_role','userroles','{user_role_id} {role_name}');
				 
			//$crud->change_field_type('password', 'password');
			//$crud->set_rules('username', 'Username', 'is_unique[users.username]');
			if( $operation == 'insert_validation' || $operation == 'insert')
			{
				$crud->set_rules('username', 'Username','trim|required|xss_clean|callback_username_check');
				$crud->required_fields('username','password','mobile_user','user_role');
			}
			if($operation == 'edit')
			{
				$crud->change_field_type('username', 'readonly');
				//$crud->callback_edit_field('username', array($this, 'readonly_edit_field'));
				$crud->required_fields('password','mobile_user','user_role');
			}

			$output = $crud->render();
			
			$this->_example_output($output);
			
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	
	/*********************************
	********  User Roles Management  *******
	*********************************/
	function userroles_management($operation = null)
	{
		try{
			/* This is only for the autocompletion */
			$crud = new grocery_CRUD();

			$crud->set_theme('datatables');
			$crud->set_table('userroles');
			$crud->set_subject('User Roles');
			
			

			$crud->columns('user_role_id','role_name','mobile_user');
			$crud->display_as('user_role_id','User Role ID')
				->display_as('role_name','User Role Description')
				->display_as('mobile_user','Mobile User');
			
			$crud->callback_add_field('mobile_user',array($this,'add_field_mobile_user'));
			$crud->callback_edit_field('mobile_user',array($this,'edit_field_mobile_user'));
			
			$crud->unset_add();
            //$crud->unset_edit();
			$crud->unset_delete();
			$output = $crud->render();
			
			$this->_example_output($output);
			
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	
	/**
	Radio button for mobile user
	**/
	function add_field_mobile_user()
	{
        return ' <input type="radio" name="mobile_user" value="1" /> Yes
		<input type="radio" name="mobile_user" value="0" /> No';
	}

	
	function edit_field_mobile_user($value)
	{
        if($value==1)
		{
		return ' <input type="radio" name="mobile_user" value="1" checked="yes"/> Yes
		<input type="radio" name="mobile_user" value="0" /> No';
		}
		else
		{
		return ' <input type="radio" name="mobile_user" value="1"/> Yes
		<input type="radio" name="mobile_user" value="0" checked="yes"/> No';
		}
	}
	
	/**
	Check if username already exists
	**/	
	public function username_check($str)
	{
		  $id = $this->uri->segment(4);
		  if(!empty($id) && is_numeric($id))
		  {
		   $username_old = $this->db->where("id",$id)->get('users')->row()->username;
		   $this->db->where("username !=",$username_old);
		  }
		  
		  $num_row = $this->db->where('username',$str)->get('users')->num_rows();
		  if ($num_row >= 1)
		  {
		   $this->form_validation->set_message('username_check', 'The username already exists');
		   return FALSE;
		  }
		  else
		  {
		   return TRUE;
		  }
	}

	
	/***************************************
	********  Stock Take Management  *******
	***************************************/
	function stocktake_management($operation = null)
	{
		try{
			/* This is only for the autocompletion */
			$crud = new grocery_CRUD();

			$crud->set_theme('datatables');
			$crud->set_table('stocktake');
			$crud->set_subject('Stock Take');
			
			

			$crud->columns('stocktake_id','mobile_stocktake_id','start_datetime','end_datetime','username');
			$crud->display_as('stocktake_id','Stock Take ID')
				->display_as('mobile_stocktake_id','Mobile Stock Take ID')
				->display_as('start_datetime','Started At')
				->display_as('end_datetime','Completed At')
				->display_as('username','User Name');
			
			//$crud->callback_add_field('mobile_user',array($this,'add_field_mobile_user'));
			//$crud->callback_edit_field('mobile_user',array($this,'edit_field_mobile_user'));
			
			$crud->unset_add();
            $crud->unset_edit();
			//$crud->unset_delete();
			$output = $crud->render();
			
			$this->_example_output($output);
			
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	
	/*********************************
	********  Stock Take details  *******
	*********************************/
	function stocktakedetails_management($operation = null)
	{
		try{
			/* This is only for the autocompletion */
			$crud = new grocery_CRUD();

			$crud->set_theme('datatables');
			$crud->set_table('stocktakedetails');
			$crud->set_subject('Stock Take Details');
			
			

			$crud->columns('stocktake_details_id','stocktake_id','rfid_tag_id','aisle_id');
			$crud->display_as('stocktake_details_id','ID')
			->display_as('stocktake_id','Stock Take ID')
			->display_as('rfid_tag_id','RFID Tag ID')
			->display_as('aisle_id','Aisle ID');
			
			$crud->set_relation('aisle_id','aisles',' {aisle_number} - {aisle_description}'); 

			
			$crud->unset_add();
            $crud->unset_edit();
			//$crud->unset_delete();
			$output = $crud->render();
			
			$this->_example_output($output);
			
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	
	
	/*********************************
	********  Pick List Data  *******
	*********************************/
	function picklist_management($operation = null)
	{
		try{
			/* This is only for the autocompletion */
			$crud = new grocery_CRUD();

			$crud->set_theme('datatables');
			$crud->set_table('picklist');
			$crud->set_subject('Automatically Generated Pick List');
			
			

			$crud->columns('picklist_id','picklist_datetime','username','stocktake_id');
			$crud->display_as('picklist_id','ID')
			->display_as('picklist_datetime','Date/Time Created')
			->display_as('username','Mobile Username')
			->display_as('stocktake_id','Stock Take ID');
			
			//$crud->set_relation('aisle_id','aisles',' {aisle_number} - {aisle_description}'); 

			
			$crud->unset_add();
            $crud->unset_edit();
			$crud->unset_delete();
			$output = $crud->render();
			
			$this->_example_output($output);
			
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	
	/*********************************************
	********  Pick List Details Management  *******
	**********************************************/
	function picklistdetails_management($operation = null)
	{
		try{
			/* This is only for the autocompletion */
			$crud = new grocery_CRUD();

			$crud->set_theme('datatables');
			$crud->set_table('picklistdetails');
			$crud->set_subject('Pick List in Detail');
			
			

			$crud->columns('picklist_details_id', 'picklist_id', 'products_id', 'required_qty','aisle_id', 'warehouse_location_id');
			$crud->display_as('picklist_details_id','ID')
			->display_as('picklist_id','Pick List / Stock-take ID / Username')
			->display_as('products_id','Product')
			->display_as('required_qty','Required Quantity')
			->display_as('aisle_id','Aisle')
			->display_as('warehouse_location_id','Warehouse Location');
			
			$crud->set_relation('products_id','products',' {products_id} - {products_name}'); 
			$crud->set_relation('picklist_id','picklist',' {picklist_id} ,{stocktake_id} ,{username}'); 
			$crud->set_relation('aisle_id','aisles',' {aisle_number} - {aisle_description}'); 
			
			$crud->unset_add();
            $crud->unset_edit();
			$crud->unset_delete();
			$output = $crud->render();
			
			$this->_example_output($output);
			
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	
	
	
	
	
	
	/*********************************
	********  Aisle Management  *******
	*********************************/
	
	function aisle_management()
	{
		try{
			/* This is only for the autocompletion */
			$crud = new grocery_CRUD();

			$crud->set_theme('datatables');
			$crud->set_table('aisles');
			$crud->set_subject('Aisle Record');
			$crud->required_fields('aisle_description','aisle_number','aisle_status');
			$crud->columns('aisle_description','aisle_number','aisle_status');
			$crud->display_as('aisle_description','Aisle Description')
				 ->display_as('aisle_number','Aisle Number')
				 ->display_as('aisle_status','Aisle Status');
			
			
			
			/*$crud->callback_add_field('aisle_description',array($this,'add_field_aisle_description'));
			$crud->callback_edit_field('aisle_description',array($this,'add_field_aisle_description'));
			
			$crud->callback_add_field('aisle_number',array($this,'add_field_aisle_number'));
			$crud->callback_edit_field('aisle_number',array($this,'add_field_aisle_number'));*/
			
			
			$crud->callback_add_field('aisle_status',array($this,'add_field_aisle_status'));
			$crud->callback_edit_field('aisle_status',array($this,'edit_field_aisle_status'));

				 
			$output = $crud->render();
			
			$this->_example_output($output);
			
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	
	
	/**
		Radio buttons Aisles
	**/
	function add_field_aisle_status()
	{
        return ' <input type="radio" name="aisle_status" value="1" /> Active
		<input type="radio" name="aisle_status" value="0" /> Inactive';
	}
	function edit_field_aisle_status($value)
	{
        if($value==1)
		{
		return ' <input type="radio" name="aisle_status" value="1" checked="checked"/> Active
		<input type="radio" name="aisle_status" value="0" /> Inactive';
		}
		else
		{
		return ' <input type="radio" name="aisle_status" value="1" /> Active
		<input type="radio" name="aisle_status" value="0" checked="yes"/> Inactive';
		}
	}
	
	
	function add_field_aisle_description()
	{
		return '<input type="text" maxlength="50" value="" name="aisle_description" style="width:462px">';
	}
	function add_field_aisle_number()
	{
		return '<input type="text" maxlength="20" value="" name="aisle_number" style="width:100px">';
	}
	
	
	
	/*********************************
	********  RFID Tag data   *******
	*********************************/
	
	function rfid_tagdata()
	{
		try{
			/* This is only for the autocompletion */
			$crud = new grocery_CRUD();

			$crud->set_theme('datatables');
			$crud->set_table('rfidtagdata');
			$crud->set_subject('RFID Tag data');
			$crud->required_fields('rfid_tag_id','products_id');
			$crud->columns('rfid_tag_id','products_id');
			$crud->display_as('rfid_tag_id','RFID Tag ID / EPC')
				 ->display_as('products_id','Product');
			
			$crud->set_relation('products_id','products','{products_id} {products_name}');
				 
			$output = $crud->render();
			
			$this->_example_output($output);
			
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	
	/***************************************
	********  Products Management  *******
	***************************************/
	function products()
	{
		try{
			/* This is only for the autocompletion */
			$crud = new grocery_CRUD();

			$crud->set_theme('datatables');
			$crud->set_table('products');
			$crud->set_subject('Products');
			$crud->required_fields('products_name','products_description','manufacturer','sku');
			$crud->columns('products_name','products_description','manufacturer','sku');
			$crud->display_as('products_name','Product Name')
				 ->display_as('products_description','Product Description')
				 ->display_as('manufacturer','Manufacturer')
				 ->display_as('sku','SKU');
				 
			//$crud->callback_add_field('products_description',array($this,'add_field_products_description'));
			//$crud->callback_edit_field('products_description',array($this,'add_field_products_description'));
				 
			$output = $crud->render();
			
			$this->_example_output($output);
			
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	
	function add_field_products_description()
	{
		return '<input type="text" maxlength="50" value="" name="aisle_description">';
	}

	
	/*********************************
	********  Products Inventory  *******
	*********************************/
	
	function products_inventory($operation = null)
	{
		try{
			/* This is only for the autocompletion */
			$crud = new grocery_CRUD();

			$crud->set_theme('flexigrid');
			$crud->set_table('productsinventory');
			$crud->set_subject('Products Inventory');
			
			$crud->columns('products_id','products_quantity','min_order_quantity','max_shelf_quantity','min_shelf_quantity','aisle_id','warehouse_location_id');
			$crud->display_as('products_id','Product ID / Name')
				 ->display_as('products_quantity','Current Stock')
				 ->display_as('min_order_quantity','Minimum Order Quantity')
				 ->display_as('max_shelf_quantity','Maximum Qty On The Shelf')
				 ->display_as('min_shelf_quantity','Minimum Qty On The Shelf')
				 ->display_as('aisle_id','Aisle Number')
				 ->display_as('warehouse_location_id','Warehouse Location ID');
			

			$crud->set_rules('products_id', 'Product ID / Name', 'is_unique[productsinventory.products_id]');

			$crud->set_relation('products_id','products','{products_id} {products_name}');
			$crud->set_relation('aisle_id','aisles',' {aisle_number} - {aisle_description}'); 
			
			if($operation == 'edit')
			{
				$crud->change_field_type('products_id', 'readonly');
				//$crud->callback_edit_field('username', array($this, 'readonly_edit_field'));
				//$crud->required_fields('password','mobile_user','user_role');
				$crud->required_fields('products_quantity','min_order_quantity','max_shelf_quantity','min_shelf_quantity','aisle_id');
			}
			else
			{
				$crud->required_fields('products_id','products_quantity','min_order_quantity','max_shelf_quantity','min_shelf_quantity','aisle_id');
			}
			
			
			
			//$crud->callback_edit_field('products_id', array($this, 'readonly_edit_products_inventory'));
			
			$output = $crud->render();
			
			$this->_example_output($output);
			
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	

	
}