# CLAUDE.md — CrossroadTwin 프로젝트 지시 문서

Behavioral guidelines to reduce common LLM coding mistakes.  
**Tradeoff:** These guidelines bias toward caution over speed. For trivial tasks, use judgment.

---

## 1. Think Before Coding

**Don't assume. Don't hide confusion. Surface tradeoffs.**

Before implementing:
- State your assumptions explicitly. If uncertain, ask.
- If multiple interpretations exist, present them - don't pick silently.
- If a simpler approach exists, say so. Push back when warranted.
- If something is unclear, stop. Name what's confusing. Ask.

---

## 2. Simplicity First

**Minimum code that solves the problem. Nothing speculative.**

- No features beyond what was asked.
- No abstractions for single-use code.
- No "flexibility" or "configurability" that wasn't requested.
- No error handling for impossible scenarios.
- If you write 200 lines and it could be 50, rewrite it.

Ask yourself: "Would a senior engineer say this is overcomplicated?" If yes, simplify.

---

## 3. Surgical Changes

**Touch only what you must. Clean up only your own mess.**

When editing existing code:
- Don't "improve" adjacent code, comments, or formatting.
- Don't refactor things that aren't broken.
- Match existing style, even if you'd do it differently.
- If you notice unrelated dead code, mention it - don't delete it.

When your changes create orphans:
- Remove imports/variables/functions that YOUR changes made unused.
- Don't remove pre-existing dead code unless asked.

The test: Every changed line should trace directly to the user's request.

---

## 4. Goal-Driven Execution

**Define success criteria. Loop until verified.**

Transform tasks into verifiable goals:
- "Add validation" → "Write tests for invalid inputs, then make them pass"
- "Fix the bug" → "Write a test that reproduces it, then make it pass"
- "Refactor X" → "Ensure tests pass before and after"

For multi-step tasks, state a brief plan:
```
1. [Step] → verify: [check]
2. [Step] → verify: [check]
3. [Step] → verify: [check]
```

Strong success criteria let you loop independently.  
Weak criteria ("make it work") require constant clarification.

---

## 5. 프로젝트 개요

**CrossroadTwin**: 학교 앞 3거리 교차로 신호 시스템의 Unity 디지털 트윈

```
실제 하드웨어:
  PLC: Mitsubishi R02CPU
  신호등: LED 6개 (보행자 3 + 차량 3)
  센서: IR 센서 3개 (XA0, XA1, XA2)
  버튼: 노약자 버튼 3개 + 긴급 버튼 + 비 버튼

Unity 앱 역할:
  - 실제 PLC와 SLMP TCP 통신으로 실시간 동기화
  - PLC 없을 때 내부 시뮬레이션 모드로 동작
  - HMI 버튼 → SLMP 쓰기로 PLC 제어
```

---

## 6. 기술 스택

```
Unity 버전:  6000.4.0f1 (Unity 6)
렌더 파이프라인: Universal Render Pipeline (URP) 17.4.0
입력 시스템: New Input System 1.19.0
내비게이션: AI Navigation 2.0.11
3D 모델링: ProBuilder 6.0.9
```

---

## 7. 개발 커맨드

### 프로젝트 열기
```
Unity Hub → Unity 6000.4.0f1 로 프로젝트 열기
코드 에디터: Visual Studio 또는 Rider
```

### 빌드
```
File → Build Settings → Build
플랫폼: PC, Mac & Linux Standalone (Windows x86_64)
```

### 테스트
```
Window → General → Test Runner
테스트 파일 위치: Assets/Tests/ (미생성 시 신규 생성)
```

---

## 8. 폴더 구조 규칙

**반드시 이 구조를 따를 것. 임의로 폴더를 생성하지 말 것.**

```
Assets/
├── Scenes/
│   └── MainScene.unity       ← 메인 씬
├── Scripts/
│   ├── Core/                 ← 페이즈 로직, 신호 제어만
│   ├── Communication/        ← SLMP 통신만
│   ├── UI/                   ← UI 제어만
│   └── Sensors/              ← 센서 로직만
├── Prefabs/
│   ├── Signals/              ← 신호등 프리팹
│   └── Vehicles/             ← 차량 프리팹
├── Models/
│   └── Intersection/         ← Meshy AI 교차로 모델
├── Materials/
├── Textures/
├── ScriptableObjects/
├── Settings/                 ← URP 렌더러 등 프로젝트 설정
├── Tests/                    ← Unity Test Runner 테스트
└── docs/                     ← 지식 저장소
    ├── CLAUDE.md
    ├── device-map.md
    ├── phase-timing.md
    └── signal-spec.md
```

---

## 9. 페이즈 타이밍 (절대 변경 금지)

**이 타이밍이 프로젝트의 핵심이다. 임의로 수정하지 말 것.**

```
전체 사이클: 90초

페이즈 A (1~21초, 21초):
  - 10→ 좌회전 녹색 + 적색등 동시 (1~18초)
  - 5시 횡단보도 녹색 (1~18초)
  - 황색 (19~21초)

페이즈 B (22~48초, 27초):
  - 12→5 좌회전 녹색 + 적색등 동시 (22~45초)
  - 10시 횡단보도 녹색 (22~45초)
  - 황색 (46~48초)

페이즈 C (49~90초, 42초):
  - 양방향 직진 녹색 (10→, 5→) (49~87초)
  - 12시 횡단보도 녹색 (49~66초)
  - 황색 (88~90초)
```

---

## 10. PLC 디바이스 맵 (SLMP 통신 기준)

**디바이스 주소를 임의로 변경하지 말 것.**

### 출력 (Y) — Unity에서 읽기

```
Y0C0: 10시 횡단보도 적색
Y0C1: 10시 횡단보도 녹색
Y0C2: 12시 횡단보도 적색
Y0C3: 12시 횡단보도 녹색
Y0C4: 5시 횡단보도 적색
Y0C5: 5시 횡단보도 녹색
Y0C6: 10시 차량 적색
Y0C7: 10시 차량 황색
Y0C8: 10시 차량 좌회전
Y0C9: 10시 차량 녹색
Y0CA: 12시 차량 적색
Y0CB: 12시 차량 황색
Y0CC: 12시 차량 좌회전
Y0CD: 5시 차량 적색
Y0CE: 5시 차량 황색
Y0CF: 5시 차량 녹색
```

### 입력 (X) — Unity에서 읽기

```
XA0: 10시 방향 차량 센서
XA1: 12시 방향 차량 센서
XA2: 5시 방향 차량 센서
```

### 내부 비트 (M) — Unity에서 읽기/쓰기

```
읽기:
  M200: 보행자 모드 활성
  M201: 긴급 모드 활성
  M203: 노약자 연장 진행 중
  M62:  차양막 상태 (펼침=ON)

쓰기 (HMI 버튼):
  M50: 10시 노약자 버튼
  M51: 12시 노약자 버튼
  M52: 5시 노약자 버튼
  M53: 긴급신호 버튼
  M54: 비 버튼 (토글)
```

### 데이터 레지스터 (D) — Unity에서 읽기/쓰기

```
읽기:
  D0:   메인 사이클 카운터 (1~90)
쓰기:
  D400: 온도 입력값 (℃)
```

---

## 11. SLMP 통신 규칙

```
프로토콜: SLMP 3E Frame
전송:     TCP/IP
기본 IP:  192.168.3.39 (SignalConfig에서 변경 가능)
포트:     5007
폴링 주기: 100ms

배치 읽기 우선:
  Y0C0~Y0CF 16비트 → 1회 배치 읽기
  개별 읽기 최소화

예외 처리 필수:
  연결 끊김 → 자동 재연결 시도
  타임아웃 → Mock 모드로 전환
```

---

## 12. 모드 시스템 규칙

**우선순위를 반드시 지킬 것.**

```
1순위 (최상): 긴급 적색 깜빡임 (M201 + 적색 단계)
2순위:        긴급 황색 (M201 + 황색 단계)
3순위:        보행자 모드 (M200) → 3곳 횡단보도 동시 녹색
4순위:        노약자 연장 (M203) → 현재 신호 유지
5순위 (기본): 일반 90초 사이클
```

---

## 13. 아키텍처 규칙

### 인터페이스 분리 (Mock ↔ 실제 PLC)

```csharp
// 반드시 이 인터페이스를 통해 신호 데이터 접근
public interface ISignalService
{
    SignalState GetSignalState();             // Y 디바이스 읽기
    SensorState GetSensorState();            // X 디바이스 읽기
    ModeState GetModeState();                // M 디바이스 읽기
    void SetBit(string device, bool value);  // M 쓰기
    void SetWord(string device, int value);  // D 쓰기
}

// 두 구현체
// MockSignalService  → PLC 없이 내부 시뮬레이션
// RealSignalService  → 실제 SLMP 통신
```

### ScriptableObject 사용 규칙

```
설정값은 반드시 SignalConfig.asset에서 관리
코드에 타이밍 숫자를 하드코딩하지 말 것

예시:
  ❌ float greenTime = 18f;
  ✅ float greenTime = signalConfig.phaseA_GreenTime;
```

### 컴포넌트 단일 책임

```
PhaseController:   페이즈 전환 로직만
SignalController:  신호등 색상 전환만
SensorController:  센서 상태 + 미감지 타이머만
UIController:      UI 업데이트만
SLMPClient:        TCP 통신만
```

### NavMesh (AI Navigation 2.0.11)

```
보행자/차량 이동에 AI Navigation 사용
NavMeshAgent는 Vehicles/, Pedestrians/ 프리팹에만 부착
신호 상태에 따른 정지/출발은 PhaseController에서 제어
```

---

## 14. 네이밍 컨벤션

```
클래스:        PascalCase      (PhaseController)
메서드:        PascalCase      (GetCurrentPhase)
변수:          camelCase       (currentPhase)
상수:          UPPER_SNAKE     (MAX_PHASE_COUNT)
ScriptableObj: PascalCase      (SignalConfig)
프리팹:        Prefab_ 접두사  (Prefab_Signal_10Clock)
씬 오브젝트:   역할_방향        (Signal_10Clock, Crosswalk_5Clock)
```

---

## 15. 금지 사항

```
❌ MonoBehaviour에 SLMP 통신 코드 직접 작성
❌ Update()에서 매 프레임 PLC 폴링
❌ 페이즈 타이밍 숫자 하드코딩
❌ 디바이스 주소 문자열 하드코딩
❌ 폴더 구조 임의 변경
❌ SignalConfig 외부에서 설정값 관리
❌ 두 개 이상의 책임을 가진 클래스 생성
❌ Unity 6000.4.0f1 이외의 버전 API 사용
```

---

## 16. 구현 단계 (진행 현황)

| 단계 | 내용 | 상태 |
|------|------|------|
| STEP 1 | 프로젝트 세팅 | ✅ 완료 |
| STEP 2 | 3D 씬 제작 | ⬜ 대기 |
| STEP 3 | ScriptableObject 설계 | ⬜ 대기 |
| STEP 4 | 핵심 로직 (시뮬레이션) | ⬜ 대기 |
| STEP 5 | UI 제작 | ⬜ 대기 |
| STEP 6 | SLMP 통신 구현 | ⬜ 대기 |
| STEP 7 | Mock ↔ PLC 전환 | ⬜ 대기 |
| STEP 8 | 차량/보행자 애니메이션 | ⬜ 대기 |
| STEP 9 | 연동 테스트 | ⬜ 대기 |
| STEP 10 | 마무리 및 빌드 | ⬜ 대기 |

---

**These guidelines are working if:**  
fewer unnecessary changes in diffs, fewer rewrites due to overcomplication,  
and clarifying questions come before implementation rather than after mistakes.