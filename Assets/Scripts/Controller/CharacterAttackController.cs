using Model;
using View;

namespace Controller
{
    public interface ICharacterAttackController
    {
        
    }
    public class CharacterAttackController : ICharacterAttackController
    {
        private ICharacterAttackModel _characterAttackModel;
        private ICharacterAttackView _characterAttackView;
        
        public CharacterAttackController(ICharacterAttackModel characterAttackModel, ICharacterAttackView characterAttackView)
        {
            _characterAttackModel = characterAttackModel;
            _characterAttackView = characterAttackView;

            characterAttackView.OnAttackClicked += HandleAttackClicked;
            characterAttackModel.OnCharacterAttacked += HandleCharacterAttack;
            characterAttackView.OnAttackFinished += HandleAttackFinished;
        }
        
        private void HandleAttackClicked(object sender, CharacterAttackClickedEventArgs e)
        {
            _characterAttackModel.Attack = true;
        }

        private void HandleCharacterAttack(object sender, CharacterAttackEventArgs e)
        {
            _characterAttackView.Attack = _characterAttackModel.Attack;
        }
        
        private void HandleAttackFinished(object sender, CharacterAttackFinishedEventArgs e)
        {
            _characterAttackModel.Attack = false;
        }
    }
}