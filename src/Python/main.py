import sys
import time
import signal
import os

from typing import Any


def _exit_gracefully(_signum: int, _frame: Any):
    """Ctrl+C 조합이나 SIGINT 이벤트에 대한 처리를 담당하는 처리기
    :param _signum: 종료 시그널의 종류
    :param _frame: 처리기 함수 주소
    """
    print("Waiting 10 seconds")
    time.sleep(10)

    # 원래 사용하던 핸들러로 되돌려놓지 않으면 부작용이 발생하므로 아래 코드는 지우면 안됨
    signal.signal(signal.SIGINT, original_sigint)

    try:
        print("Terminating program by SIGINT")
        sys.exit(0)
    except KeyboardInterrupt:
        print("Terminating program by Keyboard Interrupt")
        sys.exit(0)

    # 만약 위의 Try 문에서 다른 오류가 발생하지 않고 이 부분에 오는 로직이 있다면 아래 주석을 해제해야 함
    # signal.signal(signal.SIGINT, exit_gracefully)


def main():
    print("Hello, World!")
    while True: time.sleep(0.1)


if __name__ == '__main__':
    # Ctrl+C 등의 Terminate 시그널을 처리하는 핸들러로 대체함
    original_sigint = signal.getsignal(signal.SIGINT)
    signal.signal(signal.SIGINT, _exit_gracefully)
    main()
