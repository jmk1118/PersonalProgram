using System;
using System.IO;
using System.Collections.Generic;

namespace ExtensionChangeProgram
{
    class ExtensionChangeProgram
    {
        static string originalExtension; // 변경 전 확장자
        static string changeExtension; // 변경 후 확장자

        static void Main(string[] args)
        {
            string[] input;
            string directoryName; // 확장자를 바꿀 파일들이 있는 폴더명
            try
            {
                input = Console.ReadLine().Split(" :: ");
                directoryName = input[0];
                originalExtension = input[1];
                changeExtension = input[2];
            }
            catch
            {
                Console.WriteLine("폴더 주소 :: 변경 전 확장자 :: 변경 후 확장자 형태로 입력해주세요");
                return;
            }

            List<string> directories = new List<string>(); // 탐색할 폴더 리스트
            ChangeFiles(directoryName); // 입력받은 폴더에 있는 파일들 확장자 변경
            
            directories.AddRange(Directory.GetDirectories(directoryName));
            for (int i = 0; i < directories.Count; i++) 
            {
                ChangeFiles(directories[i]); // 입력받은 폴더의 하위 폴더들에 있는 파일들 확장자 변경
                directories.AddRange(Directory.GetDirectories(directories[i])); // 하위 폴더의 하위 폴더들을 탐색하고 탐색할 폴더 리스트에 추가
            }

            Console.WriteLine("끝났습니다.");
            Console.ReadLine();
        }

        static void ChangeFiles(string directoryName) // 폴더 내의 파일들의 확장자를 변경한다
        {
            string[] files = Directory.GetFiles(directoryName); // 폴더에 들어있는 파일들
            string newname;
            foreach (string file in files) // 파일들 중 변경 전 확장자인 파일을 변경 후 확장자로 변경
            {
                if (file.Contains("." + originalExtension))
                {
                    newname = file.Remove(file.Length - originalExtension.Length) + changeExtension;
                    File.Move(file, newname);
                }
            }
        }
    }
}
