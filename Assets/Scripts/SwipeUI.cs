using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SungHwan
{
    public class SwipeUI : MonoBehaviour
    {
        // Scrollbar�� ��ġ �������� ���� ������ �˻�
        [SerializeField]
        private Scrollbar scrollbar;
        // ���� �������� ��Ÿ���� Image Transform
        [SerializeField]
        private Transform[] circleContents;
        // �������� Swipe �Ǵ� �ð�
        [SerializeField]
        private float swipeTime = 0.2f;
        // Swipe�� ���� �ּ� �̵� �Ÿ�
        [SerializeField]
        private float swipeMinDistance = 50.0f;

        private float[] scrollPageValues;
        private float valueDistance = 0;

        private int currentPage = 0;
        private int maxPage = 0;

        private float startTouchX;
        private float endTouchX;

        private bool isSwipeMode = false;
        private float circleContentScale = 1.5f;

        private void Awake()
        {
            // �������� �� value���� �����ϴ� �迭 �޸� �Ҵ�
            scrollPageValues = new float[transform.childCount];

            // ������ ����
            valueDistance = 1f / (scrollPageValues.Length - 1f);

            // �� ������ value ��ġ ���� [0 <= value <= 1]
            for (int i = 0; i < scrollPageValues.Length; i++)
            {
                scrollPageValues[i] = valueDistance * i;
            }

            // �ִ� �������� ��
            maxPage = transform.childCount;
        }
        void Start()
        {
            // ���� ���۽� 0�� ������ ����
            SetScrollBarValue(0);
        }

        public void SetScrollBarValue(int index)
        {
            currentPage = index;
            scrollbar.value = scrollPageValues[index];
        }

        void Update()
        {
            /// <summary>
            /// ����, �ϴ� Swipe
            /// </summary>
            UpdateInput();
            UpdateCircleContent();
        }

        private void UpdateInput()
        {
            if (isSwipeMode == true)
                return;

#if UNITY_EDITOR
            // ��Ŭ�� �� 1ȸ
            // ��ġ ���� ���� ( Swipe ���� ���� )
            if (Input.GetMouseButtonDown(0))
            {
                startTouchX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                endTouchX = Input.mousePosition.x;
                UpdateSwipe();
            }
#endif
        }

        private void UpdateSwipe()
        {
            // �ʹ� ���� �Ÿ��� �������� ���� Swipe X
            // ���� �������� Swipe�ؼ� ���ư�
            if (Mathf.Abs(startTouchX - endTouchX) < swipeMinDistance)
            {
                StartCoroutine(OnSwipeOneStep(currentPage));
                return;
            }
            // Swipe ����
            bool isLeft = startTouchX < endTouchX ? true : false;

            // �̵������� ������ ��
            // ���� �������� ���� ���̸� ? ���� : ���� ������ -1
            // �̵������� �������� ��
            // ���� �������� ������ ���̸� ? ���� : ���� ������ +1
            if (isLeft == true)
            {
                if (currentPage == 0) return;
                currentPage--;
            }
            else
            {
                if (currentPage == maxPage - 1) return;
                currentPage++;
            }
            // currentIndex��°�� Swipe �̵�
            StartCoroutine(OnSwipeOneStep(currentPage));
        }

        /// <summary>
        /// �� �� ������ �ѱ�� Swipe ȿ�� ���
        /// </summary>
        private IEnumerator OnSwipeOneStep(int index)
        {
            float start = scrollbar.value;
            float current = 0;
            float percent = 0;

            isSwipeMode = true;

            while (percent < 1)
            {
                current += Time.deltaTime;
                percent = current / swipeTime;

                scrollbar.value = Mathf.Lerp(start, scrollPageValues[index], percent);

                yield return null;
            }

            isSwipeMode = false;
        }

        private void UpdateCircleContent()
        {
            /// <summary>
            /// �Ʒ��� ��ġ�� ������ ��ư ũ��, ���� ����
            /// ( ���� �ӹ��� �ִ� �������� ��ư�� ���� )
            /// ������ ���� �Ѿ�� ���� ������ ���� �ٲ�
            /// </summary>
            for (int i = 0; i < scrollPageValues.Length; ++i)
            {
                circleContents[i].localScale = Vector2.one;
                circleContents[i].GetComponent<Image>().color = Color.white;

                if (scrollbar.value < scrollPageValues[i] + (valueDistance / 2) &&
                     scrollbar.value > scrollPageValues[i] - (valueDistance / 2))
                {
                    circleContents[i].localScale = Vector2.one * circleContentScale;
                    circleContents[i].GetComponent<Image>().color = Color.black;
                }
            }
        }
    }
}

