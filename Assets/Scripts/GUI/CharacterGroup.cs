using UnityEngine;
using System.Collections;

public class CharacterGroup : MonoBehaviour
{

    private CharacterEntry selectedCharacter;

    public void Select(CharacterEntry character)
    {
        if (selectedCharacter == character) return;
        if (selectedCharacter != null) selectedCharacter.Select(false);
        selectedCharacter = character;
        character.Select(true);
    }
}
