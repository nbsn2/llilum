//
// Copyright (c) Microsoft Corporation.    All rights reserved.
//

#define USE_LLVM_INTRINSICS

namespace Microsoft.Zelig.Runtime
{
    using System;
    using System.Runtime.CompilerServices;

    using TS    = Microsoft.Zelig.Runtime.TypeSystem;
    using ISA   = TargetModel.ArmProcessor.InstructionSetVersion;


    [ExtendClass(typeof(System.Threading.Interlocked), NoConstructors=true, PlatformVersionFilter=(ISA.Platform_Version__ARMv7_all | ISA.Platform_Version__x86))]
    public static class InterlockedImpl
    {
        //
        // Helper Methods
        //

        public static int Increment( ref int location )
        {
            return InternalAdd( ref location, 1 );
        }

        public static long Increment( ref long location )
        {
            using(SmartHandles.InterruptState.DisableAll())
            {
                return ++location;
            }
        }

        public static int Decrement( ref int location )
        {
            return InternalAdd( ref location, -1 );
        }

        public static long Decrement( ref long location )
        {
            using(SmartHandles.InterruptState.DisableAll())
            {
                return --location;
            }
        }

        public static int Exchange( ref int location1 ,
                                        int value     )
        {
            return InternalExchange( ref location1, value );
        }

        public static long Exchange( ref long location1 ,
                                         long value     )
        {
            using(SmartHandles.InterruptState.DisableAll())
            {
                long oldValue = location1;

                location1 = value;

                return oldValue;
            }
        }

        public static float Exchange( ref float location1 ,
                                          float value     )
        {
            return InternalExchange( ref location1, value );
        }

        public static double Exchange( ref double location1 ,
                                           double value     )
        {
            using(SmartHandles.InterruptState.DisableAll())
            {
                double oldValue = location1;

                location1 = value;

                return oldValue;
            }
        }

        public static Object Exchange( ref Object location1 ,
                                           Object value     )
        {
            return InternalExchange( ref location1, value );
        }

        public static IntPtr Exchange( ref IntPtr location1, IntPtr value )
        {
            return InternalExchange( ref location1, value );
        }

        public static T Exchange<T>( ref T location1 ,
                                         T value     ) where T : class
        {
            return InternalExchange( ref location1, value );
        }

        //--//

        public static int CompareExchange( ref int location1 ,
                                               int value     ,
                                               int comparand )
        {
            return InternalCompareExchange( ref location1, value, comparand );
        }

        public static long CompareExchange( ref long location1 ,
                                                long value     ,
                                                long comparand )
        {
            using(SmartHandles.InterruptState.DisableAll())
            {
                long oldValue = location1;

                if(oldValue == comparand)
                {
                    location1 = value;
                }

                return oldValue;
            }
        }

        public static float CompareExchange( ref float location1 ,
                                                 float value     ,
                                                 float comparand )
        {
            return InternalCompareExchange( ref location1, value, comparand );
        }

        public static double CompareExchange( ref double location1 ,
                                                  double value     ,
                                                  double comparand )
        {
            using(SmartHandles.InterruptState.DisableAll())
            {
                double oldValue = location1;

                if(oldValue == comparand)
                {
                    location1 = value;
                }

                return oldValue;
            }
        }

        public static Object CompareExchange( ref Object location1 ,
                                                  Object value     ,
                                                  Object comparand )
        {
            return InternalCompareExchange( ref location1, value, comparand );
        }

        public static IntPtr CompareExchange( ref IntPtr location1 ,
                                                  IntPtr value     ,
                                                  IntPtr comparand )
        {
            return InternalCompareExchange( ref location1, value, comparand );

        }

        public static T CompareExchange<T>( ref T location1 ,
                                                T value     ,
                                                T comparand ) where T : class
        {
            return InternalCompareExchange( ref location1, value, comparand );

        }

        public static int Add( ref int location1 ,
                                   int value     )
        {
            return InternalAdd( ref location1, value );
        }

        public static long Add( ref long location1 ,
                                    long value     )
        {
            using(SmartHandles.InterruptState.DisableAll())
            {
                long res = location1 + value;

                location1 = res;

                return res;
            }
        }

        //--//

#if USE_LLVM_INTRINSICS
        [NoInline] // Disable inlining so we always have a chance to replace the method.
#else
        [Inline]
#endif
        [TS.WellKnownMethod( "InterlockedImpl_InternalExchange_int" )]
        internal static int InternalExchange( ref int location1,
                                                  int value )
        {
#if USE_LLVM_INTRINSICS
            BugCheck.Assert( false, BugCheck.StopCode.InvalidOperation );
            return 0;
#else
            using(SmartHandles.InterruptState.DisableAll( ))
            {
                int oldValue = location1;

                location1 = value;

                return oldValue;
            }
#endif
        }

#if USE_LLVM_INTRINSICS
        [NoInline] // Disable inlining so we always have a chance to replace the method.
#else
        [Inline]
#endif
        [TS.WellKnownMethod( "InterlockedImpl_InternalExchange_float" )]
        internal static float InternalExchange( ref float location1,
                                                    float value )
        {
#if USE_LLVM_INTRINSICS
            BugCheck.Assert( false, BugCheck.StopCode.InvalidOperation );
            return 0.0f;
#else
            using(SmartHandles.InterruptState.DisableAll( ))
            {
                float oldValue = location1;

                location1 = value;

                return oldValue;
            }
#endif
        }

#if USE_LLVM_INTRINSICS
        [NoInline] // Disable inlining so we always have a chance to replace the method.
#else
        [Inline]
#endif
        [TS.WellKnownMethod( "InterlockedImpl_InternalExchange_IntPtr" )]
        internal static IntPtr InternalExchange( ref IntPtr location1,
                                                     IntPtr value )
        {
#if USE_LLVM_INTRINSICS
            BugCheck.Assert( false, BugCheck.StopCode.InvalidOperation );
            return IntPtr.Zero;
#else
            using(SmartHandles.InterruptState.DisableAll( ))
            {
                IntPtr oldValue = location1;

                location1 = value;

                return oldValue;
            }
#endif
        }

#if USE_LLVM_INTRINSICS
        [NoInline] // Disable inlining so we always have a chance to replace the method.
#else
        [Inline]
#endif
        [TS.WellKnownMethod( "InterlockedImpl_InternalExchange_Template" )]
        [TS.DisableAutomaticReferenceCounting]
        internal static T InternalExchange<T>( ref T location1,
                                                   T value ) where T : class
        {
#if USE_LLVM_INTRINSICS
            BugCheck.Assert( false, BugCheck.StopCode.InvalidOperation );
            return null;
#else
            using(SmartHandles.InterruptState.DisableAll( ))
            {
                T oldValue = location1;

                location1 = value;

                return oldValue;
            }
#endif
        }

#if USE_LLVM_INTRINSICS
        [NoInline] // Disable inlining so we always have a chance to replace the method.
#else
        [Inline]
#endif
        [TS.WellKnownMethod( "InterlockedImpl_InternalCompareExchange_int" )]
        internal static int InternalCompareExchange( ref int location1,
                                                         int value,
                                                         int comparand )
        {
#if USE_LLVM_INTRINSICS
            BugCheck.Assert( false, BugCheck.StopCode.InvalidOperation );
            return 0;
#else
            using(SmartHandles.InterruptState.DisableAll( ))
            {
                int oldValue = location1;

                if(oldValue == comparand)
                {
                    location1 = value;
                }

                return oldValue;
            }
#endif
        }
        
#if USE_LLVM_INTRINSICS
        [NoInline] // Disable inlining so we always have a chance to replace the method.
#else
        [Inline]
#endif
        [TS.WellKnownMethod( "InterlockedImpl_InternalCompareExchange_float" )]
        internal static float InternalCompareExchange( ref float location1,
                                                           float value,
                                                           float comparand )
        {
#if USE_LLVM_INTRINSICS
            BugCheck.Assert( false, BugCheck.StopCode.InvalidOperation );
            return 0.0f;
#else
            using(SmartHandles.InterruptState.DisableAll( ))
            {
                float oldValue = location1;

                if(oldValue == comparand)
                {
                    location1 = value;
                }

                return oldValue;
            }
#endif
        }

#if USE_LLVM_INTRINSICS
        [NoInline] // Disable inlining so we always have a chance to replace the method.
#else
        [Inline]
#endif
        [TS.WellKnownMethod( "InterlockedImpl_InternalCompareExchange_IntPtr" )]
        internal static IntPtr InternalCompareExchange( ref IntPtr location1,
                                                            IntPtr value,
                                                            IntPtr comparand )
        {
#if USE_LLVM_INTRINSICS
            BugCheck.Assert( false, BugCheck.StopCode.InvalidOperation );
            return IntPtr.Zero;
#else
            using(SmartHandles.InterruptState.DisableAll( ))
            {
                IntPtr oldValue = location1;

                if(oldValue == comparand)
                {
                    location1 = value;
                }

                return oldValue;
            }
#endif
        }

#if USE_LLVM_INTRINSICS
        [NoInline] // Disable inlining so we always have a chance to replace the method.
#else
        [Inline]
#endif
        [TS.WellKnownMethod( "InterlockedImpl_InternalCompareExchange_Template" )]
        [TS.DisableAutomaticReferenceCounting]
        internal static T InternalCompareExchange<T>( ref T location1,
                                                          T value,
                                                          T comparand ) where T : class
        {
#if USE_LLVM_INTRINSICS
            BugCheck.Assert( false, BugCheck.StopCode.InvalidOperation );
            return null;
#else
            using(SmartHandles.InterruptState.DisableAll( ))
            {
                T oldValue = location1;

                if(Object.ReferenceEquals( oldValue, comparand ))
                {
                    location1 = value;
                }

                return oldValue;
            }
#endif
        }

#if USE_LLVM_INTRINSICS
        [NoInline] // Disable inlining so we always have a chance to replace the method.
#else
        [Inline]
#endif
        [TS.WellKnownMethod( "InterlockedImpl_InternalAdd_int" )]
        internal static int InternalAdd( ref int location1,
                                             int value )
        {
#if USE_LLVM_INTRINSICS
            BugCheck.Assert( false, BugCheck.StopCode.InvalidOperation );
            return 0;
#else
            using(SmartHandles.InterruptState.DisableAll( ))
            {
                int res = location1 + value;

                location1 = res;

                return res;
            }
#endif
        }
    }
}

