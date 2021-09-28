using UnityEngine;

namespace Assets.Scripts.BoatSelector
{
    public class BoatIdentifier : MonoBehaviour
    {

        public int BoatId;

        public GameObject BoatManager;

        public void SelectBoat()
        {
            BoatSelectorManager manager = BoatManager.GetComponent<BoatSelectorManager>();
            manager.ActiveBoatPlacement((Boats)BoatId);
        }

        public enum Boats
        {
            CARRIER = 1,
            BATTLESHIP = 2,
            CRUISER = 3,
            SUBMARINE = 4,
            DESTROYER = 5,
            EMPTY = 6
        }

    }
}
