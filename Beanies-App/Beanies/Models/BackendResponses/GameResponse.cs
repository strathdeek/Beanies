using System;
using System.Collections.Generic;
using System.Text;

namespace Beanies.Models.BackendResponses
{
    class GameResponse
    {
        public Game game { get; set; }

        public bool success { get; set; }

        public string msg { get; set; }

        public List<Game> games { get; set; }
    }
}
