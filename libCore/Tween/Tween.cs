// namespace LibCore
// {
//
//     // A sample can be found in Test/TestTween
//     public class Tween : Manager<Tween>
//     {
//         private List<Tweener> _runningList;
//         private List<Tweener> _waitingList;
//
//         public Tween()
//         {
//             _runningList = new List<Tweener>();
//             _waitingList = new List<Tweener>();
//         }
//
//         public static Tweener CreateAndPlay(float start, float end, float duration)
//         {
//             var tweener = Tweener.Create(start, end, duration);
//             Play(tweener);
//             return tweener;
//         }
//
//         public static Tweener CreateAndPlay(Vector2 start, Vector2 end, float duration)
//         {
//             var tweener = Tweener.Create(start, end, duration);
//             Play(tweener);
//             return tweener;
//         }
//
//         public static Tweener CreateAndPlay(Vector3 start, Vector3 end, float duration)
//         {
//             var tweener = Tweener.Create(start, end, duration);
//             Play(tweener);
//             return tweener;
//         }
//
//         public static Tweener CreateAndPlay(Vector4 start, Vector4 end, float duration)
//         {
//             var tweener = Tweener.Create(start, end, duration);
//             Play(tweener);
//             return tweener;
//         }
//
//         public static Tweener CreateAndPlay(Quaternion start, Quaternion end, float duration)
//         {
//             var tweener = Tweener.Create(start, end, duration);
//             Play(tweener);
//             return tweener;
//         }
//
//         public static Tweener CreateAndPlay(Color start, Color end, float duration)
//         {
//             var tweener = Tweener.Create(start, end, duration);
//             Play(tweener);
//             return tweener;
//         }
//
//         public static Tweener CreateAndPlay(TweenValue start, TweenValue end, float duration)
//         {
//             var tweener = Tweener.Create(start, end, duration);
//             Play(tweener);
//             return tweener;
//         }
//
//         public static void Play(Tweener tweener)
//         {
//             Instance._waitingList.Add(tweener);
//         }
//
//         public static void Stop(Tweener tweener)
//         {
//             tweener.Stop();
//         }
//
//         public static void Pause(Tweener tweener)
//         {
//             tweener.Pause();
//         }
//
//         // public void Tick(float deltaTime)
//         protected void OnUpdate(float deltaTime)
//         {
//             for (int i = Instance._runningList.Count - 1; i >= 0; i--)
//             {
//                 Tweener tweener = Instance._runningList[i];
//                 if (tweener.IsPaused)
//                     continue;
//
//                 tweener.Update(deltaTime);
//                 if (!tweener.IsPlaying)
//                 {
//                     Instance._runningList.RemoveAt(i);
//                     // tweener.Dispose();
//                 }
//             }
//
//             for (int i = Instance._waitingList.Count - 1; i >= 0; i--)
//             {
//                 Tweener tweener = Instance._waitingList[i];
//                 if (tweener.IsStop)
//                 {
//                     Instance._waitingList.RemoveAt(i);
//                     continue;
//                 }
//
//                 tweener.UpdateDelayTime(deltaTime);
//                 if (tweener.IsReady)
//                 {
//                     Instance._waitingList.RemoveAt(i);
//                     Instance._runningList.Add(tweener);
//                     tweener.Play();
//                     continue;
//                 }
//                 if (!tweener.IsWaiting)
//                 {
//                     Instance._waitingList.RemoveAt(i);
//                 }
//             }
//         }
//     }
//
// }