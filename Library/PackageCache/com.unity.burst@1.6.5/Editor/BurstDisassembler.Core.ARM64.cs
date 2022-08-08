#if UNITY_EDITOR || BURST_INTERNAL
namespace Unity.Burst.Editor
{
    internal partial class BurstDisassembler
    {
        private class ARM64AsmTokenKindProvider : AsmTokenKindProvider
        {
            private static readonly string[] Registers = new[]
            {
                "nzcv",
                "wsp",
                "sp",

                "r0",
                "r1",
                "r2",
                "r3",
                "r4",
                "r5",
                "r6",
                "r7",
                "r8",
                "r9",
                "r10",
                "r11",
                "r12",
                "r13",
                "r14",
                "r15",
                
                "b0",
                "b1",
                "b2",
                "b3",
                "b4",
                "b5",
                "b6",
                "b7",
                "b8",
                "b9",
                "b10",
                "b11",
                "b12",
                "b13",
                "b14",
                "b15",
                "b16",
                "b17",
                "b18",
                "b19",
                "b20",
                "b21",
                "b22",
                "b23",
                "b24",
                "b25",
                "b26",
                "b27",
                "b28",
                "b29",
                "b30",
                "b31",
                "d0",
                "d1",
                "d2",
                "d3",
                "d4",
                "d5",
                "d6",
                "d7",
                "d8",
                "d9",
                "d10",
                "d11",
                "d12",
                "d13",
                "d14",
                "d15",
                "d16",
                "d17",
                "d18",
                "d19",
                "d20",
                "d21",
                "d22",
                "d23",
                "d24",
                "d25",
                "d26",
                "d27",
                "d28",
                "d29",
                "d30",
                "d31",
                "h0",
                "h1",
                "h2",
                "h3",
                "h4",
                "h5",
                "h6",
                "h7",
                "h8",
                "h9",
                "h10",
                "h11",
                "h12",
                "h13",
                "h14",
                "h15",
                "h16",
                "h17",
                "h18",
                "h19",
                "h20",
                "h21",
                "h22",
                "h23",
                "h24",
                "h25",
                "h26",
                "h27",
                "h28",
                "h29",
                "h30",
                "h31",
                "q0",
                "q1",
                "q2",
                "q3",
                "q4",
                "q5",
                "q6",
                "q7",
                "q8",
                "q9",
                "q10",
                "q11",
                "q12",
                "q13",
                "q14",
                "q15",
                "q16",
                "q17",
                "q18",
                "q19",
                "q20",
                "q21",
                "q22",
                "q23",
                "q24",
                "q25",
                "q26",
                "q27",
                "q28",
                "q29",
                "q30",
                "q31",
                "s0",
                "s1",
                "s2",
                "s3",
                "s4",
                "s5",
                "s6",
                "s7",
                "s8",
                "s9",
                "s10",
                "s11",
                "s12",
                "s13",
                "s14",
                "s15",
                "s16",
                "s17",
                "s18",
                "s19",
                "s20",
                "s21",
                "s22",
                "s23",
                "s24",
                "s25",
                "s26",
                "s27",
                "s28",
                "s29",
                "s30",
                "s31",
                "w0",
                "w1",
                "w2",
                "w3",
                "w4",
                "w5",
                "w6",
                "w7",
                "w8",
                "w9",
                "w10",
                "w11",
                "w12",
                "w13",
                "w14",
                "w15",
                "w16",
                "w17",
                "w18",
                "w19",
                "w20",
                "w21",
                "w22",
                "w23",
                "w24",
                "w25",
                "w26",
                "w27",
                "w28",
                "w29",
                "w30",
                "wzr",
                "x0",
                "x1",
                "x2",
                "x3",
                "x4",
                "x5",
                "x6",
                "x7",
                "x8",
                "x9",
                "x10",
                "x11",
                "x12",
                "x13",
                "x14",
                "x15",
                "x16",
                "x17",
                "x18",
                "x19",
                "x20",
                "x21",
                "x22",
                "x23",
                "x24",
                "x25",
                "x26",
                "x27",
                "x28",
                "x29",
                "x30",
            };

            private static readonly string[] Qualifiers = new[]
            {
                // Comparison qualifiers
                "eq",
                "ne",
                "hs",
                "lo",
                "mi",
                "pl",
                "vs",
                "vc",
                "hi",
                "ls",
                "ge",
                "lt",
                "gt",
                "le",
                "al",
                "nv",
            };

            private static readonly string[] Instructions = new[]
            {
                "abs",
                "adc",
                "add",
                "addhn",
                "addhn2",
                "addp",
                "addv",
                "adr",
                "adrp",
                "aesd",
                "aese",
                "aesimc",
                "aesmc",
                "and",
                "asr",
                "at",
                "b",
                "beq",
                "bfi",
                "bfm",
                "bfxil",
                "bge",
                "bgt",
                "bic",
                "bif",
                "bit",
                "bl",
                "ble",
                "blr",
                "blx",
                "bne",
                "br",
                "brk",
                "bsl",
                "cbnz",
                "cbz",
                "ccmn",
                "ccmp",
                "clrex",
                "cls",
                "clz",
                "cmeq",
                "cmge",
                "cmgt",
                "cmhi",
                "cmhs",
                "cmle",
                "cmlt",
                "cmn",
                "cmp",
                "cmtst",
                "cnt",
                "crc32b",
                "crc32cb",
                "crc32ch",
                "crc32cw",
                "crc32cx",
                "crc32h",
                "crc32w",
                "crc32x",
                "csel",
                "csinc",
                "csinv",
                "csneg",
                "dc",
                "dcps1",
                "dcps2",
                "dcps3",
                "dmb",
                "drps",
                "dsb",
                "dup",
                "eon",
                "eor",
                "eret",
                "ext",
                "extr",
                "fabd",
                "fabs",
                "facge",
                "facgt",
                "fadd",
                "faddp",
                "fccmp",
                "fccmpe",
                "fcmeq",
                "fcmge",
                "fcmgt",
                "fcmle",
                "fcmlt",
                "fcmp",
                "fcmpe",
                "fcsel",
                "fcvt",
                "fcvtas",
                "fcvtau",
                "fcvtl",
                "fcvtl2",
                "fcvtms",
                "fcvtmu",
                "fcvtn",
                "fcvtn2",
                "fcvtns",
                "fcvtnu",
                "fcvtps",
                "fcvtpu",
                "fcvtxn",
                "fcvtxn2",
                "fcvtzs",
                "fcvtzu",
                "fdiv",
                "fmadd",
                "fmax",
                "fmaxnm",
                "fmaxnmp",
                "fmaxnmv",
                "fmaxp",
                "fmaxv",
                "fmin",
                "fminnm",
                "fminnmp",
                "fminnmv",
                "fminp",
                "fminv",
                "fmla",
                "fmls",
                "fmov",
                "fmsub",
                "fmul",
                "fmulx",
                "fneg",
                "fnmadd",
                "fnmsub",
                "fnmul",
                "frecpe",
                "frecps",
                "frecpx",
                "frinta",
                "frinti",
                "frintm",
                "frintn",
                "frintp",
                "frintx",
                "frintz",
                "frsqrte",
                "frsqrts",
                "fsqrt",
                "fsub",
                "hint",
                "hlt",
                "hvc",
                "ic",
                "ins",
                "isb",
                "ld1",
                "ld1r",
                "ld2",
                "ld2r",
                "ld3",
                "ld3r",
                "ld4",
                "ld4r",
                "ldar",
                "ldarb",
                "ldarh",
                "ldaxp",
                "ldaxr",
                "ldaxrb",
                "ldaxrh",
                "ldnp",
                "ldp",
                "ldpsw",
                "ldr",
                "ldrb",
                "ldrd",
                "ldrh",
                "ldrsb",
                "ldrsh",
                "ldrsw",
                "ldtr",
                "ldtrb",
                "ldtrh",
                "ldtrsb",
                "ldtrsh",
                "ldtrsw",
                "ldur",
                "ldurb",
                "ldurh",
                "ldursb",
                "ldursh",
                "ldursw",
                "ldxp",
                "ldxr",
                "ldxrb",
                "ldxrh",
                "lsl",
                "lsls",
                "lsr",
                "madd",
                "mla",
                "mls",
                "mneg",
                "mov",
                "movi",
                "movk",
                "movn",
                "movt",
                "movz",
                "movw",
                "mrs",
                "msr",
                "msub",
                "mul",
                "mvn",
                "mvni",
                "neg",
                "ngc",
                "nop",
                "not",
                "orn",
                "orr",
                "pmul",
                "pmull",
                "pmull2",
                "prfm",
                "prfum",
                "raddhn",
                "raddhn2",
                "rbit",
                "ret",
                "rev",
                "rev16",
                "rev32",
                "rev64",
                "ror",
                "rshrn",
                "rshrn2",
                "rsubhn",
                "rsubhn2",
                "saba",
                "sabal",
                "sabal2",
                "sabd",
                "sabdl",
                "sabdl2",
                "sadalp",
                "saddl",
                "saddl2",
                "saddlp",
                "saddlv",
                "saddw",
                "saddw2",
                "sbc",
                "sbfiz",
                "sbfm",
                "sbfx",
                "scvtf",
                "sdiv",
                "sev",
                "sevl",
                "sha1c",
                "sha1h",
                "sha1m",
                "sha1p",
                "sha1su0",
                "sha1su1",
                "sha256h",
                "sha256h2",
                "sha256su0",
                "sha256su1",
                "shadd",
                "shl",
                "shll",
                "shll2",
                "shrn",
                "shrn2",
                "shsub",
                "sli",
                "smaddl",
                "smax",
                "smaxp",
                "smaxv",
                "smc",
                "smin",
                "sminp",
                "sminv",
                "smlal",
                "smlal2",
                "smlsl",
                "smlsl2",
                "smnegl",
                "smov",
                "smsubl",
                "smulh",
                "smull",
                "smull2",
                "sqabs",
                "sqadd",
                "sqdmlal",
                "sqdmlal2",
                "sqdmlsl",
                "sqdmlsl2",
                "sqdmulh",
                "sqdmull",
                "sqdmull2",
                "sqneg",
                "sqrdmulh",
                "sqrshl",
                "sqrshrn",
                "sqrshrn2",
                "sqrshrun",
                "sqrshrun2",
                "sqshl",
                "sqshlu",
                "sqshrn",
                "sqshrn2",
                "sqshrun",
                "sqshrun2",
                "sqsub",
                "sqxtn",
                "sqxtn2",
                "sqxtun",
                "sqxtun2",
                "srhadd",
                "sri",
                "srshl",
                "srshr",
                "srsra",
                "sshl",
                "sshll",
                "sshll2",
                "sshr",
                "ssra",
                "ssubl",
                "ssubl2",
                "ssubw",
                "ssubw2",
                "st1",
                "st2",
                "st3",
                "st4",
                "stlr",
                "stlrb",
                "stlrh",
                "stlxp",
                "stlxr",
                "stlxrb",
                "stlxrh",
                "stnp",
                "stp",
                "str",
                "strb",
                "strh",
                "sttr",
                "sttrb",
                "sttrh",
                "stur",
                "sturb",
                "sturh",
                "stxp",
                "stxr",
                "stxrb",
                "stxrh",
                "sub",
                "subs",
                "subhn",
                "subhn2",
                "suqadd",
                "svc",
                "sxtb",
                "sxth",
                "sxtw",
                "sys",
                "sysl",
                "tbl",
                "tbnz",
                "tbx",
                "tbz",
                "tlbi",
                "trn1",
                "trn2",
                "tst",
                "uaba",
                "uabal",
                "uabal2",
                "uabd",
                "uabdl",
                "uabdl2",
                "uadalp",
                "uaddl",
                "uaddl2",
                "uaddlp",
                "uaddlv",
                "uaddw",
                "uaddw2",
                "ubfiz",
                "ubfm",
                "ubfx",
                "ucvtf",
                "udiv",
                "uhadd",
                "uhsub",
                "umaddl",
                "umax",
                "umaxp",
                "umaxv",
                "umin",
                "uminp",
                "uminv",
                "umlal",
                "umlal2",
                "umlsl",
                "umlsl2",
                "umnegl",
                "umov",
                "umsubl",
                "umulh",
                "umull",
                "umull2",
                "uqadd",
                "uqrshl",
                "uqrshrn",
                "uqrshrn2",
                "uqshl",
                "uqshrn",
                "uqshrn2",
                "uqsub",
                "uqxtn",
                "uqxtn2",
                "urecpe",
                "urhadd",
                "urshl",
                "urshr",
                "ursqrte",
                "ursra",
                "ushl",
                "ushll",
                "ushll2",
                "ushr",
                "usqadd",
                "usra",
                "usubl",
                "usubl2",
                "usubw",
                "usubw2",
                "uxtb",
                "uxth",
                "uzp1",
                "uzp2",
                "wfe",
                "wfi",
                "xtn",
                "xtn2",
                "yield",
                "zip1",
                "zip2",
            };

            private static readonly string[] SimdInstructions = new string[]
            {
                // TODO: add missing instructions
                "vabs",
                "vadd",
                "vmov",
                "vmul",
                "vsub",
            };

            private ARM64AsmTokenKindProvider() : base(Registers.Length + Qualifiers.Length + Instructions.Length + SimdInstructions.Length)
            {
                foreach (var register in Registers)
                {
                    AddTokenKind(register, AsmTokenKind.Register);
                }

                foreach (var instruction in Qualifiers)
                {
                    AddTokenKind(instruction, AsmTokenKind.Qualifier);
                }

                foreach (var instruction in Instructions)
                {
                    AddTokenKind(instruction, AsmTokenKind.Instruction);
                }

                foreach (var instruction in SimdInstructions)
                {
                    AddTokenKind(instruction, AsmTokenKind.InstructionSIMD);
                }
            }

            public static readonly ARM64AsmTokenKindProvider Instance = new ARM64AsmTokenKindProvider();
        }
    }
}
#endif