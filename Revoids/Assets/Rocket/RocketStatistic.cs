using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RocketStatistic : MonoBehaviour
{
    [Header("Scene References")]
    public TMP_Text scoreText;
    public Transform fieldCrew;
    public GameObject memberPrefab;

    [Header("Statisic")]
    public int score = 0;
    public int lives = 3;
    public int crewMemeber = 0;

    /// <summary>
    /// Fügt den Member deinem Raumschiff hinzu und zerstört diesen
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="pickupedMember"></param>
    public void MemberEnterSpaceShip(Collision2D collision, GameObject pickupedMember)
    {
        // Den Collsion2D brauchen wir um zu unterscheiden bei welchem Object diese funktion ausgeführt wird
        // Das GameObject brauchen wir nachher um den Member zu zerstören der dieses Skript aufruft
        
        if (collision.gameObject.CompareTag("Player"))
        {
            // Füge einer Member hinzu um Logik auszuführen mit der Punktzahl
            crewMemeber += 1;

            // Füge Scorepoints hinzu
            score += 2000;

            // Ändere die Scoreanzeige auf die aktuellen Scorepoints
            scoreText.text = $"Score: {score.ToString("0000000")}";

            // Erstelle einen Crew mitglied zur UI anzeige
            GameObject member = Instantiate(memberPrefab);

            // Füge den zuvor erstellen Member dem UI Container hinuu
            member.transform.SetParent(fieldCrew);
            
            // Zerstöre den Member aus der Szene
            Destroy(pickupedMember);
        }
    }
}
