using UnityEngine;

namespace GUIManager
{
    public class CharacterDisplayManager : SingletonBase<CharacterDisplayManager>
    {
        [SerializeField] private CharacterInfo[] characters;

        [SerializeField] private GameObject spawnPoint;

        private int characterIndex = 0;

        [SerializeField] public CharacterInfo currentCharacterType = null;

        private CharacterInfo currentCharacter = null;

        private Animator anim;

        protected override void Initialize()
        {
            base.Initialize();
        }

        private void OnEnable()
        {
            if (spawnPoint != null)
            {
                SetCurrentCharacterType(characterIndex);
            }
        }

        private void OnDisable()
        {
            if (currentCharacterType != null)
            {
                Destroy(currentCharacterType.gameObject);
            }
        }

        public void SetCurrentCharacterType(int index)
        {
            if (currentCharacterType != null)
            {
                Destroy(currentCharacterType.gameObject);
            }

            //  Get our character information
            CharacterInfo character = characters[index];
            currentCharacterType = Instantiate<CharacterInfo>(character,
                                                              spawnPoint.transform.position,
                                                              Quaternion.identity);

            anim = currentCharacterType.GetComponentInChildren<Animator>();

            currentCharacterType.transform.LookAt(Camera.main.transform);
            characterIndex = index;
        }

        public void CreateCharacter()
        {
            currentCharacter = Instantiate<CharacterInfo>(currentCharacterType,
                                                          spawnPoint.transform.position,
                                                          Quaternion.identity);

            currentCharacter.gameObject.SetActive(false);

            DontDestroyOnLoad(currentCharacter);
        }

        private CharacterInfo GetCurrentCharacter()
        {
            return currentCharacter;
        }

        public Animator CharacterAnimator()
        {
            return anim;
        }
    }
}