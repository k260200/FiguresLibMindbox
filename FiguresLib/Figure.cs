using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLib
{
    /*
        При данной постановке задачи класс пуст, однако он объединяет все классы-фигуры в общую иерархию.

        Если бы требуемый функционал был шире, в этом классе можно было бы например хранить позицию фигур
    и другие поля.
        
        Также в этом классе можно было бы реализовать все требуемые интерфейсы, например гипотетический IDrawable.

        ISquareCalculatable не реализуется здесь т.к. неизвестно считать ли фигурами объекты без площади (отрезок и точка)
    а также трёхмерные объекты.
     */
    public abstract class Figure
    {
    }
}
