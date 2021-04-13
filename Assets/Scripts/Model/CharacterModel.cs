using UnityEngine;

namespace Model
{
    public interface ICharacterModel
    {
        GameObject Character { get; }
        ICharacterStatModel CharacterStatModel { get; }
    }
    public class CharacterModel : ICharacterModel
    {
        public GameObject Character { get; }
        public ICharacterStatModel CharacterStatModel { get; }
        
        // Character construction
        public CharacterModel(GameObject character, ICharacterStatModel characterStatModel)
        {
            // Character game object
            Character = character;
            // Character stats
            CharacterStatModel = characterStatModel;
            characterStatModel.Character = character;
            // Add character to characters
            CharactersInfoModel.CharacterModels.Add(this);
        }
        
        
        // When character destruct delete from list
        public void Destroy()
        {
            CharactersInfoModel.CharacterModels.Remove(this);
        }
    }
}