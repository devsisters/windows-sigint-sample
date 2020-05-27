#include <stdio.h>
#include <stdlib.h>
#include <signal.h>
#include <tchar.h>
#include <windows.h>

#define DEFAULT_SIGINT_WAIT_SECONDS (60 * 5)

void sigint_handler(int);

static _crt_signal_t pPreviousSigIntHandler = NULL;
static int nWaitSeconds = DEFAULT_SIGINT_WAIT_SECONDS;

int _tmain(int argc, _TCHAR **argv) {
    _TCHAR* pWaitSecondsBuffer;
    size_t nLength;
    errno_t nError;

    nError = _tdupenv_s(&pWaitSecondsBuffer, &nLength, _T("WAIT_SECONDS"));
    if (nError) {
        _ftprintf_s(stderr, _T("Falling back to default wait second value (%d).\r\n"), DEFAULT_SIGINT_WAIT_SECONDS);
        nWaitSeconds = DEFAULT_SIGINT_WAIT_SECONDS;
    }
    else {
        _ftprintf_s(stdout, _T("Wait second specified. (%s).\r\n"), pWaitSecondsBuffer);
        nWaitSeconds = max(1, _ttoi(pWaitSecondsBuffer));
        free(pWaitSecondsBuffer);
    }

    _ftprintf_s(stdout, _T("Wait second: %d sec.\r\n"), nWaitSeconds);

    _ftprintf_s(stdout, _T("Hello, World!\r\n"));
    pPreviousSigIntHandler = signal(SIGINT, sigint_handler);

    Sleep(INFINITE);
    return 0;
}

void sigint_handler(int s) {
    if (s != SIGINT) {
        return;
    }

    _ftprintf_s(stdout, _T("SIGINT (%ld) reeceived.\r\n"), s);

    for (int i = nWaitSeconds; i > 0; i--) {
        _ftprintf_s(stdout, _T("%ld second(s) remained to interrupt.\r\n"), i);
        Sleep(1000);
    }

    _ftprintf_s(stdout, _T("SIGINT (%ld) processed.\r\n"), s);
    signal(SIGINT, pPreviousSigIntHandler);
    exit(0);
}
