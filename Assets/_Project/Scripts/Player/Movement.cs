using System.Collections;
using UnityEngine;

namespace Subway.Player
{
    /*          ПАМЯТКА (нейминг переменных)
     *          
     * private: _variableName;
     * protected: VariableName;
     * public: VariableName;
     * 
     * private/protected/public const: VariableName / VARIABLE_NAME
     * 
     * -------------------------------------------------------------
     *              (ОЧЕРЕДНОСТЬ МЕТОДОВ)
     * 
     * 1)   public ctor() : конструктор, если есть возможность
     * 2)   private void Awake()/Update()/... : любые приватные Unity сообщения
     * 3)   public void Fun() : наши публичные методы  
     * 4)   private void InnerFun() : наши приватные методы для внутреннего использования
     * 5)   IEnumerator SimpleRoutine() : корутины удобнее в самом конце держать, 
     * область видимости не указывается (как например в полях интерфейса). 
     * Исключение - класс утилит-корутин, к которым обращаются другие классы.
     * 
     * -------------------------------------------------------------
     *              (ОЧЕРЕДНОСТЬ ПЕРЕМЕННЫХ) тут на самом деле по удобству
     * 
     * 1) public 
     * 2) [SerializeField] private
     * 3) protected
     * 4) private
     * 5) UnityEvent/Action
     * 6) const и пр.
     * 
     * 
     * --------------------------------------------------------------
     *               (ОСОБЕННОСТИ КОДСТАЙЛА ENUM/INTERFACE)
     * 
     * public enum TypeEnumerable { First = 0, Second, ... } : CamelCase тип, капслоком не нужно
     * public interface IMoveable
     * {
     *      float Speed { get; }
     *      
     *      void Stop();            // Область видимости не указывается, очевидный public
     * }
     */

    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [Header("Настройки передвижения")]
        [SerializeField] private float _speed;
        [SerializeField] private float _slideSpeed;
        [SerializeField] private float _gravityForce;
        [SerializeField] private float _jumpForce;
        [SerializeField] private bool _isAlive;

        [Header("Ускорение падения")]
        [SerializeField] private float _jumpVelocityFalloff;
        [SerializeField] private float _fallofMultiplier;

        private Animator _animator;
        private CharacterController _characterController;

        [SerializeField] private Vector3 _velocity;

        private int _previousHandledInput;

        private int _currentLine = 0;
        private float _slideT;
        [SerializeField] private bool _isSliding;
        private const int LineOffsetX = 2;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();

            // Указываем вектор скорости движения, z направление не меняем.
            _velocity = transform.forward * _speed;
        }

        private void Update()
        {
            // Заглушка, потом уберем
            if (_isAlive == false)
                return;

            ProcessSwipe();
            ApplyGravity();
            ProcessJump();

            MovePlayer();
        }

        private void MovePlayer()
        {
            _characterController.Move(_velocity * Time.deltaTime);
        }

        private void ProcessJump()
        {
            if (_characterController.isGrounded == false)
                return;

            bool jumpInput = Input.GetKeyDown(KeyCode.Space);

            if (jumpInput == true)
                _velocity.y += _jumpForce;
        }

        private void ApplyGravity()
        {
            // Как отрефакторить эту страшилу?
            if (_characterController.isGrounded == false)
            {
                var gravityPerFrame = _gravityForce * Time.deltaTime;

                if (_velocity.y < _jumpVelocityFalloff)
                    gravityPerFrame *= _fallofMultiplier;

                _velocity.y += gravityPerFrame;
            }
            else
            {
                _velocity.y = _gravityForce;
            }
        }

        private void ProcessSwipe()
        {
            if (_isSliding == true)
                return;

            // Забавно, но на клавиатуре иногда работает smoothing
            int horizontalInput = (int)Input.GetAxisRaw("Horizontal");

            if (horizontalInput == 0)
                return;

            // Заглушка, потом тык в стену будем обрабатывать через stumble (споткнуться)
            _currentLine = Mathf.Clamp(_currentLine + horizontalInput, -1, 1);
            StartCoroutine(SlideRoutine());
        }

        IEnumerator SlideRoutine()
        {
            _isSliding = true;
            var destinationX = _currentLine * LineOffsetX;
            _velocity.x = (destinationX - transform.position.x) * _slideSpeed;

            var completePath = 0f;
            while (completePath < LineOffsetX)
            {
                yield return null;
                completePath += Mathf.Abs(_velocity.x * Time.deltaTime);
            }

            _isSliding = false;
        }
    }
}
