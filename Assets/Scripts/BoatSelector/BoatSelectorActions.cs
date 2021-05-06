using Assets.Scripts.Game.Network.Packets.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.BoatSelector
{
    class BoatSelectorActions : MonoBehaviour
    {

        public void Continue()
        {
            Board board = new Board();
            foreach (KeyValuePair<BoatIdentifier.Boats, BoatPosition> entry in BoatSelectorManager._mapPlacements)
            {
                board.boats.Add(entry.Value);
            }

            BoatSelectorManager.Board = board;
            new BoatPositionsPacket()
            {
                board = board
            }.Send();
            SceneManager.LoadScene(6);
        }

    }
}
