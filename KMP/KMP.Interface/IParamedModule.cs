using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KMP.Interface.Model;
using Inventor;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Infranstructure.Events;

namespace KMP.Interface
{
  public  interface IParamedModule
    {

        ParameterBase Parameter { get; set; }
        string ModelPath { get; set; }
        string ProjectType { get; set; }
        string FullPath { get; }
        string Name { get; set; }
        //ComponentOccurrence Occurrence { get; set; }
        string PreviewImagePath { get; }
        ModuleCollection SubParamedModules { get; set; }

        IParamedModule FindModule(string projType);
        bool AddModule(IParamedModule pm);
        void CreateModule();
        bool CheckParamete();
        void InitModule();
        void InitCreatedModule();

        string GetValueByDisplayName(IParamedModule module, string name, string par);
        string GetValueByDisplayName(IParamedModule module, string name, string par, string member);
        string GetPicByOrient(IParamedModule module, string orient);



        event PropertyChangedEventHandler PropertyChanged;

        event EventHandler<GeneratorEventArgs> GeneratorChanged;
        event EventHandler<GeneratorEventArgs> ParErrorHappen;
        void GeneratorProgress(object sender, string info);
        void Serialization();
        void Serialization(string path);
        IParamedModule DeSerialization(string path);
        int GetGeneratorCount();
    }
}
