<?php 
include("../db/db.php");
/**
 * 
 */
class modelo
{
	private $db;
	
	function __construct()
	{
		$this->db = new db();
	}

	function AddPaciente($parametros)
	{
		$sql = "INSERT INTO  paciente (Nombre_paciente,Apellido_paciente,FechaN_paciente,Detalle_paciente,foto_paciente) VALUE 
		('".$parametros['nombre']."','".$parametros['apellido']."','".$parametros['FechaN']."','".$parametros['detalle']."','".$parametros['foto']."') ";
			return $this->db->sql_string($sql);
	}	
	function AddTriaje($parametros)
	{
		$fecha = date('Y-m-d');
		$sql = "INSERT INTO  triaje (Id_paciente,Peso,Altura,Nivel_Insulina,Presion_arterial,Fecha) VALUE 
		('".$parametros['paciente']."','".$parametros['peso']."','".$parametros['altura']."','".$parametros['insulina']."','".$parametros['presion']."','".$fecha."') ";
			return $this->db->sql_string($sql);
	}
	function AddMedicina($parametros)
	{
		$sql = "INSERT INTO  medicamento (Nombre_medicamento,Presentacion_medicamento,UniMedida_medicamento,Dosis_medicina,Detalle_medicamento,foto_medicina) VALUE 
		('".$parametros['nombre']."','".$parametros['presentacion']."','UNI','".$parametros['dosis']."','".$parametros['descripcion']."','".$parametros['foto']."') ";
			return $this->db->sql_string($sql);
	}
	function AddDosis($parametros)
	{
		$sql = "INSERT INTO  dosis (Id_paciente,Id_medicamento,Duracion,Frecuencia,Hora) VALUE 
		('".$parametros['paciente']."','".$parametros['medicina']."','".$parametros['duracion']."','".$parametros['intervalo']."','".$parametros['hora']."') ";
			return $this->db->sql_string($sql);
	}
	function DeleteMedicina($parametros)
	{
		$sql = "UPDATE  medicamento SET Estado_medicina = 'I' WHERE Id_medicamento='".$parametros['id']."'";
			return $this->db->sql_string($sql);
	}
	function DeletePaciente($parametros)
	{
		$sql = "UPDATE  paciente SET Estado_paciente = 'I' WHERE Id_paciente='".$parametros['id']."'";
			return $this->db->sql_string($sql);
	}
	function DeleteDosis($parametros)
	{
		$sql = "UPDATE  dosis SET Estado_dosis = 'I' WHERE Id_dosis='".$parametros['id']."'";
			return $this->db->sql_string($sql);
	}
	function ListaPaciente($id=false)
	{
		$sql = "SELECT Id_paciente as 'Id',Nombre_paciente as 'Nombre',Apellido_paciente as 'Apellido', FechaN_paciente as 'FechaNac',Detalle_paciente as 'Detalle',foto_paciente as 'Foto' FROM paciente WHERE Estado_paciente = 'A'";
		if($id)
		{
			$sql.=" AND Id_paciente = '".$id."'";
		}
		return $this->db->datos($sql);
	}
	function ListaMedicina($id=false)
	{
		$sql = "SELECT Id_medicamento as 'Id',Nombre_medicamento as 'Nombre',Presentacion_medicamento as 'Presentacion',UniMedida_medicamento as 'Medida',Detalle_medicamento as 'Descripcion',foto_medicina  as 'Foto' FROM medicamento WHERE Estado_medicina = 'A'";
		if($id)
		{
			$sql.=" AND Id_medicamento = '".$id."'";
		}
		return $this->db->datos($sql);
	}
	function ListaTriaje($id)
	{
		$sql = "SELECT Id_triaje as 'Id',Nivel_insulina as 'NivelInsu',Presion_arterial as 'PresionArt',Fecha,T.Id_paciente as 'PacienteId'
			FROM triaje T 
			WHERE T.Id_paciente = '".$id."'
			ORDER BY Fecha DESC ";

			// print_r($sql);die();
		return $this->db->datos($sql);
	}
	function ListaDosis($id)
	{
		$sql = "SELECT Id_dosis as 'Id',Id_paciente as 'IdPaciente',Id_medicamento as 'IdMedicamento',CURDATE() as 'FechaInicio',Frecuencia,Duracion,'True' as 'Activo' FROM dosis WHERE Estado_dosis = 'A' AND Id_paciente = '".$id."'";
		// print_r($sql);die();
		return $this->db->datos($sql);
	}
}
?>