#$ErrorActionPreference = 'Stop'

# Convierte los ficheros indicados en el path pasado por parametros de .xls a .csv utilizando el separador por defecto de Windows
Function ConvertToCsv
{
	[CmdletBinding()]
	Param
	(
		[Parameter(Mandatory=$true)][String]$Folder
	)
	$ExcelFiles = Get-ChildItem -Path $Folder -Filter *.xls -Recurse

	$excelApp = New-Object -ComObject Excel.Application
	$excelApp.DisplayAlerts = $false

	Write-Host "Buscando ficheros .xls para convertirlos a .csv"

	$ExcelFiles | ForEach-Object {
		if ([IO.Path]::GetExtension($_.FullName) -eq ".xls")
		{
			Write-Host "Convirtiendo " $_.FullName
			$workbook = $excelApp.Workbooks.Open($_.FullName, [Microsoft.Office.Interop.Excel.XlPlatform]::xlWindows)
			$csvFilePath = $_.FullName -replace "\.xls$", ".csv"

			$workbook.SaveAs($csvFilePath, [Microsoft.Office.Interop.Excel.XlFileFormat]::xlCSVWindows)
			$workbook.Close()
		}
	}

	# Release Excel Com Object resource
	$excelApp.Workbooks.Close()
	$excelApp.Visible = $false
	$excelApp.Quit()
	[System.Runtime.Interopservices.Marshal]::ReleaseComObject($excelApp) | Out-Null
}

# Modifica el separador por defecto de Windows para poder utilizar el que necesitamos (|)
function ToggleListSeparator
{
	[CmdletBinding()]
	Param
	(
		[Parameter(Mandatory=$true)][String]$NewSeparator
	)
	
    $path = "hkcu:\Control Panel\International"
    $key = "sList"

    $cur_sep = (Get-ItemProperty -path $path -name $key).$key

    Set-ItemProperty -path $path -name $key -Value $NewSeparator -type string
    $new_sep = (Get-ItemProperty -path $path -name $key).$key

    Write-Output "Separador (campo $path.$key ) cambiado de '$cur_sep' a '$new_sep'"
}

$path = "hkcu:\Control Panel\International"
$key = "sList"
$initialSeparator = (Get-ItemProperty -path $path -name $key).$key

$FolderPath = Get-Location

ToggleListSeparator -NewSeparator "|"
ConvertToCsv -Folder $FolderPath
ToggleListSeparator -NewSeparator $initialSeparator

Read-Host "Proceso terminado. Pulsa Enter para finalizar"