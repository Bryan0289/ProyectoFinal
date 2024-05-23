<?php 
include("../Modelo/modelo.php");


$controlador = new controlador();

// if($_SERVER['REQUEST_METHOD'] == 'GET')
// {
	if(isset($_GET['ListaPaciente']))
	{
		$parametros = array();
		if(isset($_POST['parametros']))
		{
			$parametros = $_POST['parametros'];
		}
		echo json_encode($controlador->ListaPaciente($parametros));
	}
	if(isset($_GET['ListaMedicina']))
	{
		$parametros = array();
		if(isset($_POST['parametros']))
		{
			$parametros = $_POST['parametros'];
		}
		echo json_encode($controlador->ListaMedicina($parametros));
	}
	if(isset($_GET['ListaTriaje']))
	{
		$parametros = array();
		if(isset($_GET))
		{
			$parametros = $_GET;
		}
		echo json_encode($controlador->ListaTriaje($parametros));
	}
	if(isset($_GET['ListaDosis']))
	{
		$parametros = array();
		if(isset($_GET))
		{
			$parametros = $_GET;
		}
		echo json_encode($controlador->ListaDosis($parametros));
	}

	// =======================================================//

	if(isset($_GET['AddPaciente']))
	{
		$parametros = array();
		if(isset($_POST))
		{
			$parametros = $_POST;
		}
		echo json_encode($controlador->AddPaciente($parametros));
	}

	if(isset($_GET['AddMedicina']))
	{
		$parametros = array();
		if(isset($_POST))
		{
			$parametros = $_POST;
		}
		echo json_encode($controlador->AddMedicina($parametros));
	}
	if(isset($_GET['DeleteMedicina']))
	{
		$parametros = array();
		if(isset($_POST))
		{
			$parametros = $_POST;
		}
		echo json_encode($controlador->DeleteMedicina($parametros));
	}
	if(isset($_GET['DeletePaciente']))
	{
		$parametros = array();
		if(isset($_POST))
		{
			$parametros = $_POST;
		}
		echo json_encode($controlador->DeletePaciente($parametros));
	}
	if(isset($_GET['DeleteDosis']))
	{
		$parametros = array();
		if(isset($_POST))
		{
			$parametros = $_POST;
		}
		echo json_encode($controlador->DeleteDosis($parametros));
	}
	if(isset($_GET['AddTriaje']))
	{
		$parametros = array();
		if(isset($_POST))
		{
			$parametros = $_POST;
		}
		echo json_encode($controlador->AddTriaje($parametros));
	}
	if(isset($_GET['AddDosis']))
	{
		$parametros = array();
		if(isset($_POST))
		{
			$parametros = $_POST;
		}
		echo json_encode($controlador->AddDosis($parametros));
	}
	



	// $parametros = $_GET;

// }

/*
if ($_SERVER['REQUEST_METHOD'] == 'POST')
{
    $parametros = $_POST;
}
if ($_SERVER['REQUEST_METHOD'] == 'DELETE')
{
	$parametros = $_GET;
}

if ($_SERVER['REQUEST_METHOD'] == 'PUT')
{
	$codigo = $_GET;
	$parametros = $_POST;
}
*/

/**
 * 
 */
class controlador
{
	private $modelo;
	function __construct()
	{
		$this->modelo = new modelo();
	}


	function ListaPaciente($parametros)
	{
		$datos = $this->modelo->ListaPaciente();
		return $datos;
		// print_r($datos);die();
	}
	function ListaMedicina($parametros)
	{
		$datos = $this->modelo->ListaMedicina();
		return $datos;

	}
	function ListaTriaje($parametros)
	{
		$id = $parametros['Id'];
		$datos = $this->modelo->ListaTriaje($id);
		foreach ($datos as $key => $value) {
			$datosP = $this->modelo->ListaPaciente($id);
			$datos[$key]['Paciente'] = $datosP[0];
		}

		// print_r($datos);die();
		return $datos;

	}
	function ListaDosis($parametros)
	{
		$id = $parametros['Id'];
		$datos = $this->modelo->ListaDosis($id);
		foreach ($datos as $key => $value) {
			$datosP = $this->modelo->ListaPaciente($id);
			$datosM = $this->modelo->ListaMedicina($value['IdMedicamento']);
			$datos[$key]['Paciente'] = $datosP[0];
			$datos[$key]['Medicina'] = $datosM[0];
		}
// print_r($datos);die();
		return $datos;

	}
	function AddPaciente($parametros)
	{
		return $this->modelo->AddPaciente($parametros);
	}
	function AddTriaje($parametros)
	{
		return $this->modelo->AddTriaje($parametros);
	}
	function AddMedicina($parametros)
	{
		return $this->modelo->AddMedicina($parametros);
	}
	function DeleteMedicina($parametros)
	{
		return $this->modelo->DeleteMedicina($parametros);
	}
	function DeletePaciente($parametros)
	{
		return $this->modelo->DeletePaciente($parametros);
	}
	function DeleteDosis($parametros)
	{
		return $this->modelo->DeleteDosis($parametros);
	}
	function AddDosis($parametros)
	{
		return $this->modelo->AddDosis($parametros);
	}

}
?>