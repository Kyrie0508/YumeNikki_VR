# YUMENIKKI VR Project
***

## 프로젝트 소개
본 프로젝트는 일본의 동인게임 유메닛키를 VR로 재해석한 프로젝트이다.

유메닛키의 여러 2차창작 게임들중 VR 환경에서 제작된 게임이 없었다는 점과
유메닛키 특유의 음울하고 기괴한 분위기의 맵을 탐험하고 아이템을
수집하는 특징이 VR로 재해석할 여지가 많다고 생각하여 유메닛키를
선정하여 프로젝트를 진행하게 되었다.

## 팀 구성
***
| 팀원  | 담당역할                                                                                               |
|:----|:---------------------------------------------------------------------------------------------------|
| 윤여원 | Map(Dark, Road, Ending Animation), Scene 상호작용을 통한 이동 구현, 오브젝트 아웃라인                                 |
| 연준호 | Map(Ideal Room, Basic Room, Dream Room, Outside), Character Rigging, Grab Interaction              |
| 박유상 | Map(Door Nikki, Class), NPC Tracking 구현, 숨겨진 책 속 문장 Interaction구현, 텍스쳐 제작                          |
| 최재완 | GIthub생성, Map(Title, White Desert, Eyeball, Graffiti), 배경 이미지 작업, UI Interaction, Inventory, 최종 완성 |

## 기술스택 및 구동 환경
***

**프로그래밍 언어** : C#

**개발 엔진** : Unity 2022.3.60f1

**프레임워크** : XR Plugin Management

**Git 호스팅 및 VCS** : Github

**IDE** : JetBrains Rider, Visual Studio

**Font** : MapleStory Bold, MapleStory Light

## 설치 및 게임 조작 가이드
***
### 설치 및 빌드 가이드
1. References에서 필요한 Font와 asset을 다운로드 
2. Source Code를 다운로드 한 뒤 압축 해제
3. Unity 2022.3.60f1로 압축 해제한 소스 코드를 프로젝트 단위로 열기
4. Meta Quest Link 설치 후 Meta Quest2 기기 연결
5. Build Settings에 들어간 뒤 플랫폼을 Android로 선택
6. Run Device를 연결한 Meta Quest2 기기로 선택한 후 빌드

### 버튼 가이드

<p align="center">
<img width="70%" height="70%" alt = "버튼 가이드" src ="https://github.com/user-attachments/assets/90aca46b-c652-4612-8b45-5c8036b49c64">


① 액션 버튼

양쪽 컨트롤러에 2개씩 있습니다. 왼쪽에는 X와 Y버튼, 오른쪽은 A와 B버튼입니다.

② 아날로그 스틱(썸 스틱)

컨트롤러를 쥐었을 때 엄지 손가락으로 조종할 수 있는 부위로, 아날로그 스틱 또는 썸스틱이라고 합니다.


③ 메뉴 버튼


④ 트리거

컨트롤러를 쥐었을 때 엄지와 검지 손가락을 제외한 손가락들이 위치하는 부위에 있는 버튼입니다.


⑤ 그립 버튼

컨트롤러를 쥐었을 때 집게 손가락 위치에 있는 부위에 있는 버튼입니다.


⑥ 배터리 커버



⑦ 손목 스트랩



⑧ 썸 레스트

버튼이나 아날로그 스틱을 사용하지 않을 때 엄지 손가락을 올려두는 곳입니다.



⑨ 오큘러스 버튼

### 조작 가이드

**이동** : 왼쪽 터치 컨트롤러의 아날로그 스틱으로 조작

**시점 회전** : 오른쪽 터치 컨트롤러의 아날로그 스틱으로 조작

**UI 상호작용** : 양쪽 터치 컨트롤러의 그립 버튼으로 상호작용

**인벤토리 토글** : 오른쪽 터치 컨트롤러의 A버튼으로 상호작용 

## References

**Font** : https://maplestory.nexon.com/Media/Font

**버튼 가이드** : https://bbs.ruliweb.com/community/board/300777/read/355

**Argyle** : https://assetstore.unity.com/packages/3d/environments/assets-school-hallway-101718#content

**JapaneseClassroom** : https://assetstore.unity.com/packages/3d/props/interior/japanese-classroom-1k-2k-texture-237323
