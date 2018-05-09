﻿using ImputacionHoras.Common.Logic.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImputacionHoras.Dao.Csv
{
    public interface IDaoCsv
    {
        void ExportarImputaciones(List<EntradaImputacion> listaImputaciones);
    }
}
