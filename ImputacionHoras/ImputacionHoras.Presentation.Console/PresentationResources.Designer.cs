﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImputacionHoras.PresentationConsole {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class PresentationResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal PresentationResources() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ImputacionHoras.PresentationConsole.PresentationResources", typeof(PresentationResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a C:\Users\diego.blazquez\Downloads\Product - Assets.csv.
        /// </summary>
        internal static string AssetsPathCsvDiego {
            get {
                return ResourceManager.GetString("AssetsPathCsvDiego", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a .
        /// </summary>
        internal static string ContractorsPathCsvDani {
            get {
                return ResourceManager.GetString("ContractorsPathCsvDani", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a C:\Users\diego.blazquez\Downloads\Developers - Contractor.csv.
        /// </summary>
        internal static string ContractorsPathCsvDiego {
            get {
                return ResourceManager.GetString("ContractorsPathCsvDiego", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a .
        /// </summary>
        internal static string TimesheetPathCsvDani {
            get {
                return ResourceManager.GetString("TimesheetPathCsvDani", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a C:\Users\diego.blazquez\Downloads\Timesheet Report.csv.
        /// </summary>
        internal static string TimesheetPathCsvDiego {
            get {
                return ResourceManager.GetString("TimesheetPathCsvDiego", resourceCulture);
            }
        }
    }
}
