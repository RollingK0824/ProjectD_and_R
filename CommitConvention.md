# Project_D_and_R 커밋 컨벤션

## 1. 목적

이 문서는 2인 유니티 C# 프로젝트의 Git 커밋 메시지 작성 규칙을 정의함. 다음 목적을 달성함.

* **일관성 유지**: 모든 팀원이 동일한 형식으로 커밋 메시지를 작성하여 히스토리의 가독성을 높임.
* **변경 사항 추적 용이**: 어떤 변경이 발생했는지, 왜 발생했는지, 그리고 누가 변경했는지를 쉽게 파악할 수 있도록 함.
* **협업 효율 증대**: 풀 리퀘스트(Pull Request) 리뷰 및 변경 이력 확인 시 시간을 절약하고 오류를 줄임.
* **자동화 가능성**: 향후 변경 로그 생성 또는 버전 관리 자동화에 활용할 수 있는 기반을 마련함.

## 2. 커밋 메시지 구조

커밋 메시지는 주로 다음과 같은 구조를 따름.

&lt;type>: &lt;subject>

&lt;body>

&lt;footer>


  * `<type>` (필수): 커밋의 종류를 나타냄 (아래 3. 커밋 타입 참고).
  * `<subject>` (필수): 변경 사항을 간결하게 요약함.
  * `<body>` (선택): 변경 사항에 대한 자세한 설명을 제공함.
  * `<footer>` (선택): 관련 이슈 번호, 참조 등을 포함함.

## 3\. 커밋 타입 (`<type>`)

커밋의 주요 목적을 나타내는 접두사임. 다음 타입들을 사용함.

| 타입       | 설명                                            | 예시                                      |
| :--------- | :---------------------------------------------- | :---------------------------------------- |
| `feat`     | 새로운 기능 추가                                | `feat: 플레이어 이동 기능 구현`           |
| `fix`      | 버그 수정                                       | `fix: UI 버튼 클릭 오류 수정`             |
| `docs`     | 문서 변경 (README, 주석 등)                     | `docs: README.md 업데이트`                |
| `style`    | 코드 포맷팅, 세미콜론 누락 등 (코드 동작에 영향 없음) | `style: 코드 컨벤션 적용 및 정리`         |
| `refactor` | 코드 리팩토링 (기능 변경 없이 코드 구조 개선)   | `refactor: 유틸리티 함수 분리`            |
| `test`     | 테스트 코드 추가 또는 수정                      | `test: 플레이어 공격 로직 테스트 케이스 추가` |
| `build`    | 빌드 시스템 또는 외부 종속성 변경 (Unity 버전, Package Manager 등) | `build: Unity 2022.3 버전 업그레이드`     |
| `ci`       | CI/CD 설정 파일 변경                            | `ci: GitHub Actions 워크플로우 추가`      |
| `perf`     | 성능 개선                                       | `perf: 불필요한 GC alloc 제거`            |
| `chore`    | 그 외 자잘한 변경, 빌드 프로세스 변경, 라이브러리 업데이트 등 | `chore: .gitignore 파일 업데이트`&lt;br&gt;`chore: ThirdParty 라이브러리 추가` |

## 4\. 제목 (`<subject>`) 규칙

`<subject>`는 변경 사항을 한 줄로 요약하며 다음 규칙을 따름.

  * 첫 글자는 소문자로 시작함: `feat: add player movement`
  * 과거형이 아닌 현재형 동사 사용: `fix: fix bug` (고쳤다X, 고친다O)
  * 명령문 형식 사용: `feat: implement player movement`
  * 50자 이내로 간결하게 작성: 풀 리퀘스트 목록에서 한눈에 파악하기 용이하도록 함.
  * 마침표(.)로 끝내지 않음: 간결성을 위함.
  * 변경 내용이 명확하게 드러나도록 함: 불필요한 정보는 본문에서 다룸.

**예시:**

  * `feat: add basic character controller`
  * `fix: resolve null reference exception in UIManager`
  * `refactor: optimize rendering pipeline for performance`

## 5\. 본문 (`<body>`) 규칙 (선택 사항)

`<subject>`만으로는 설명하기 어려운 자세한 내용이나 변경의 배경 등을 작성함.

  * 왜 변경했는지 (Why): 변경의 동기 또는 문제의 배경을 설명함.
  * 무엇을 변경했는지 (What): 구체적인 변경 내용을 나열함.
  * 어떻게 변경했는지 (How): 기술적인 구현 방식을 설명할 수 있음.
  * 72자 이내로 줄 바꿈: 가독성을 위함.
  * `<body>`와 `<subject>` 사이에 한 줄 비워야 함.

**예시:**

feat: implement game start screen

새로운 UI 화면을 추가하여 게임 시작 기능을 구현함.
포함된 내용은 다음과 같음:

시작 버튼
옵션 버튼
종료 버튼
사용자들이 게임 시작 진입점을 찾기 어렵다는 문제 해결함.


## 6\. 꼬리말 (`<footer>`) 규칙 (선택 사항)

관련된 이슈 트래커(GitHub Issues 등)가 있다면 해당 이슈 번호를 참조함.

  * `Closes #이슈번호`: 해당 커밋으로 이슈가 해결되어 닫힐 경우 사용함.
  * `Refs #이슈번호`: 해당 커밋이 특정 이슈와 관련이 있을 경우 사용함.
  * `Breaks #이슈번호`: 특정 이슈를 유발하거나 호환성을 깨뜨리는 경우 사용함 (가급적 사용 자제).
  * `<footer>`와 `<body>` 사이에 한 줄 비워야 함.

**예시:**

fix: correct player health bar display

체력바가 데미지 입은 후 잘못된 값을 표시했음.
이 수정으로 플레이어 현재 체력에 따라 정확하게 업데이트되도록 함.

Closes #42
Refs #15


## 7\. 유니티 프로젝트 특이사항

  * `.meta` 파일 커밋: 유니티는 에셋 생성/수정 시 `.meta` 파일을 자동으로 생성함. 이 파일들은 에셋의 고유 ID와 설정 정보를 포함하므로, **반드시 커밋에 포함**해야 함. `.meta` 파일을 커밋하지 않으면 팀원 간 프로젝트 동기화에 문제가 발생할 수 있음.
  * Scene, Prefab 변경: Scene이나 Prefab 파일을 변경했을 경우, 어떤 오브젝트의 어떤 컴포넌트가 변경되었는지 제목이나 본문에 명시하는 것이 좋음.
      * `fix: resolve missing component in Player prefab`
      * `feat: add new level structure (Scene: Level_01)`
  * Unity Package Manager (UPM) 변경: `Packages/manifest.json` 파일이 변경되는 경우 `build` 타입으로 커밋함.
      * `build: update TextMeshPro package to 3.0.6`
  * Git LFS (Large File Storage) 활용: 대용량 에셋(모델, 텍스처, 사운드 등)은 Git LFS를 사용하여 관리하는 것이 좋음. 이는 리포지토리 크기를 줄이고 클론/푸시 속도를 향상시킴.
      * Git LFS 사용 여부를 팀원과 합의하고 설정함.

## 8\. 풀 리퀘스트 (Pull Request) 작성 시

  * 커밋 컨벤션 준수: 풀 리퀘스트에 포함된 모든 커밋들이 이 컨벤션을 준수해야 함.
  * 풀 리퀘스트 제목: 풀 리퀘스트의 제목은 해당 풀 리퀘스트가 달성하는 주요 목표를 요약하여 작성함 (예: `feat: Core Combat System 구현`).
  * 풀 리퀘스트 설명: 풀 리퀘스트 자체의 설명은 해당 브랜치에서 어떤 작업을 진행했으며, 어떤 의도로 작업했는지 등을 자세히 기술함. 여러 커밋으로 이루어진 경우, 해당 커밋들이 어떤 하나의 큰 작업을 이루는지 설명함.

## 9\. 예시 커밋 메시지

feat: implement player jump mechanics

플레이어 캐릭터에 기본 점프 기능을 추가함.

Rigidbody.AddForce를 사용하여 수직 이동
플레이어가 땅에 닿아있는지 확인 후 점프 허용
수직 이동이 없던 문제 해결함.


fix: correct UI font scaling on various resolutions

UI 텍스트 요소가 1920x1080 외 해상도에서 올바르게 스케일링되지 않았음.
텍스트 잘리거나 너무 크게 보이는 문제 발생했었음.

  - Canvas Scaler 설정을 'Scale With Screen Size'로 조정
  - 특정 UI 요소의 폰트 크기 가독성 좋게 수정

Closes \#25
Refs \#15

refactor: separate input handling into dedicated class

키보드 및 마우스 입력 로직을 PlayerController에서 InputManager 클래스로 분리함.

관심사 분리를 통해 PlayerController가 캐릭터 이동 및 액션에 더 집중하도록 함.
향후 입력 방식 변경 시 수정 용이성을 높임.
