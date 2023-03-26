using System.Collections;
using Unity.VisualScripting;
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

        [Header("Запоминание следующего слайда")]
        [SerializeField] private bool _predictSlide;
        [SerializeField] private float _predictSlideTime;

        public AnimationCurve SlideCurve;

        [Header("Ускорение падения")]
        [SerializeField] private float _jumpVelocityFalloff;
        [SerializeField] private float _fallofMultiplier;

        private Animator _animator;
        private CharacterController _characterController;

        [SerializeField] private Vector3 _velocity;

        private int _predictedHandledInput;

        private int _currentLine = 0;
        private bool _isSliding;
        private float _curvedT;
        private const int LineOffsetX = 2;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();

            // Указываем вектор скорости движения, z направление не меняем.
            _velocity = transform.forward * _speed;
        }

        private void FixedUpdate()
        {
            
        }

        private void Update()
        {
            // Заглушка, потом уберем
            if (_isAlive == false)
                return;

            ProcessSwipe();
            ApplyGravity();
            ProcessJump();


            // Non-velocity based:
            if (_isSliding == true)
                _velocity.x = 0f;

            MovePlayer();
        }

        private void MovePlayer() 
            => _characterController.Move(_velocity * Time.deltaTime);

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
            // Очевидно, здесь кривые работают по тому-же самому принципу
            // Но не советую использовать, тк гравитация имеет аккумулятивный (накопительный эффект)
            // А прыжок - реактивный (единоразовый). Тут добавлять кривую - плохая практика.
            if (_characterController.isGrounded == true)
            {
                _velocity.y = _gravityForce;
                return;
            }

            float gravityPerFrame = CalculateSmoothGravity();
            _velocity.y += gravityPerFrame;
        }

        private float CalculateSmoothGravity()
        {
            var gravityPerFrame = _gravityForce * Time.deltaTime;
            if (_velocity.y < _jumpVelocityFalloff)
                gravityPerFrame *= _fallofMultiplier;

            return gravityPerFrame;
        }

        private void ProcessSwipe()
        {
            if (_isSliding == true && _predictSlide == false)
                return;

            int horizontalInput = (int)Input.GetAxisRaw("Horizontal");

            if (_predictSlide == true && _curvedT >= _predictSlideTime)
            {
                if (_predictedHandledInput == 0 && horizontalInput != 0)
                    _predictedHandledInput = horizontalInput;
            }

            if (_isSliding == true)
                return;

            // Выбираем последнюю введенную клавишу. Если не вводили, то ту, что недавно кликали
            // во время слайда.
            var resultInput = horizontalInput;
            if (horizontalInput == 0)
                resultInput = _predictedHandledInput;
            
            _currentLine = Mathf.Clamp(_currentLine + resultInput, -1, 1);
            _predictedHandledInput = 0;

            if (resultInput != 0)
                StartCoroutine(SlideRoutineCurve());
        }


        // Чекайте, могут быть мат. ошибки
        IEnumerator SlideRoutineCurve()
        {
            _isSliding = true;

            var destinationX = _currentLine * LineOffsetX;
            var startPositionX = transform.position.x;

            float t = 0f;
            _curvedT = 0f;
            while (_curvedT < 1f)
            {
                var predictT = Time.deltaTime * _slideSpeed;
                var predictCurvedT = SlideCurve.Evaluate(Mathf.Clamp(t + predictT, 0f, 1f));

                var deltaCurvedT = (predictCurvedT - _curvedT);

                t += predictT;
                _curvedT = predictCurvedT;

                // Функцию Time.deltaTime выполняет deltaCurvedT
                var slideSpeedByCurve = deltaCurvedT;
                var step = (destinationX - startPositionX) * slideSpeedByCurve * Vector3.right;

                _characterController.Move(step);
                yield return null;
            }

            // Остаток погрешности нивелируем за один кадр.
            _characterController.Move((destinationX - transform.position.x) * Vector3.right);
            yield return null;

            _isSliding = false;
        }

        IEnumerator SlideRoutineWrongCurve()
        {
            _isSliding = true;

            var destinationX = _currentLine * LineOffsetX;
            var startPositionX = transform.position.x;

            float t = 0f;
            float curvedT = 0f;
            while (curvedT < 1f)
            {
                t += Time.deltaTime * _slideSpeed;

                // Изменяем скорость анимации через время:
                // Искажаем, проецируем реальное время на кривую.
                // Работает только с линейной кривой.

                curvedT = SlideCurve.Evaluate(t);

                var slideSpeedByCurve = _slideSpeed * (1f - (t - curvedT));
                var velocity = (destinationX - startPositionX) * slideSpeedByCurve * Vector3.right;
                
                _characterController.Move(velocity * Time.deltaTime);

                yield return null;
            }

            // Остаток погрешности нивелируем за один кадр.
            _characterController.Move((destinationX - transform.position.x) * Vector3.right);
            yield return null;

            _isSliding = false;
        }


        // Без ошибки погрешности, основаный на v = s/t, скорость слайда = множителю времени
        IEnumerator SlideRoutine()
        {
            _isSliding = true;

            var destinationX = _currentLine * LineOffsetX;
            var startPositionX = transform.position.x;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * _slideSpeed;

                var velocity = (destinationX - startPositionX) * _slideSpeed * Vector3.right;
                _characterController.Move(velocity * Time.deltaTime);
                
                yield return null;
            }

            // Остаток погрешности нивелируем за один кадр.
            _characterController.Move((destinationX - transform.position.x) * Vector3.right);
            yield return null;

            _isSliding = false;
        }



        // Старый неточный вариант, основанный на скорости
        IEnumerator SlideRoutineOldVelocity()
        {
            _isSliding = true;

            var destinationX = _currentLine * LineOffsetX;
            _velocity.x = (destinationX - transform.position.x) * _slideSpeed;

            float t = 0f;
            while (t <= 1f)
            {
                yield return null;
                t += Time.deltaTime;

                _velocity.x = (destinationX - transform.position.x) * _slideSpeed;
                Debug.Log(_velocity.x + " ; " + transform.position.x);
            }

            yield return null;
            _velocity.x = 0f;
            
            
            /*
                        var completePath = 0f;
                        while (completePath < LineOffsetX)
                        {
                            yield return null;
                            completePath += Mathf.Abs(_velocity.x * Time.deltaTime);
                        }
            */
            _isSliding = false;
        }
    }
}
