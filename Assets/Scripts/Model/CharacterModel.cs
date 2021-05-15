using UnityEngine;

namespace Model
{
    public interface ICharacterModel
    {
        int Id { get; }
        Sprite HudImageSprite { get; }
        GameObject Character { get; }
        ICharacterStatModel CharacterStatModel { get; }
        ICharacterMovementModel CharacterMovementModel { get; }
    }
    public class CharacterModel : ICharacterModel
    {
        public int Id { get; }
        
        public Sprite HudImageSprite { get; }
        public GameObject Character { get; }
        public ICharacterStatModel CharacterStatModel { get; }
        public ICharacterMovementModel CharacterMovementModel { get; }

        // Character construction
        public CharacterModel(GameObject character, Sprite characterHudImage, ICharacterStatModel characterStatModel , ICharacterMovementModel characterMovementModel)
        {
            // Character game object
            Character = character;
            // Character Hud Image
            HudImageSprite = characterHudImage;
            // Character stats
            CharacterStatModel = characterStatModel;
            characterStatModel.Character = character;
            // Character movements
            CharacterMovementModel = characterMovementModel;
            // Assign id to this character
            Id = CharactersData.CharacterModels.Count;
            // Add character to characters
            CharactersData.CharacterModels.Add(this);
        }
        
        // When character destruct delete from list
        public void Destroy()
        {
            CharactersData.CharacterModels.Remove(this);
        }
    }
}