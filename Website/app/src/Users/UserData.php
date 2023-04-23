<?php

namespace App\Users;

use SilverStripe\ORM\DataObject;
use SilverStripe\Security\Permission;
use App\CharacterDatabase\CharacterHat;
use App\CharacterDatabase\CharacterTop;
use App\CharacterDatabase\CharacterEyes;
use App\CharacterDatabase\CharacterHair;
use App\CharacterDatabase\CharacterMouth;
use App\CharacterDatabase\CharacterBottom;
use App\CharacterDatabase\CharacterSkinColor;

/**
 * Class \App\Database\Experience
 *
 * @property string $Nickname
 * @property int $XP
 * @property string $UserKey
 * @property int $SelectedSkinColor
 * @property int $SelectedEyes
 * @property int $SelectedMouth
 * @property int $SelectedHair
 * @property int $SelectedBottom
 * @property int $SelectedTop
 * @property int $SelectedHat
 * @method \SilverStripe\ORM\ManyManyList|\App\CharacterDatabase\CharacterSkinColor[] SkinColors()
 * @method \SilverStripe\ORM\ManyManyList|\App\CharacterDatabase\CharacterEyes[] Eyes()
 * @method \SilverStripe\ORM\ManyManyList|\App\CharacterDatabase\CharacterMouth[] Mouths()
 * @method \SilverStripe\ORM\ManyManyList|\App\CharacterDatabase\CharacterHair[] Hairs()
 * @method \SilverStripe\ORM\ManyManyList|\App\CharacterDatabase\CharacterBottom[] Bottoms()
 * @method \SilverStripe\ORM\ManyManyList|\App\CharacterDatabase\CharacterTop[] Tops()
 * @method \SilverStripe\ORM\ManyManyList|\App\CharacterDatabase\CharacterHat[] Hats()
 */
class UserData extends DataObject
{
    private static $db = [
        "Nickname" => "Varchar(255)",
        "XP" => "Int",
        "UserKey" => "Varchar(255)",
        "SelectedSkinColor" => "Int",
        "SelectedEyes" => "Int",
        "SelectedMouth" => "Int",
        "SelectedHair" => "Int",
        "SelectedBottom" => "Int",
        "SelectedTop" => "Int",
        "SelectedHat" => "Int",
    ];

    private static $many_many = [
        "SkinColors" => CharacterSkinColor::class,
        "Eyes" => CharacterEyes::class,
        "Mouths" => CharacterMouth::class,
        "Hairs" => CharacterHair::class,
        "Bottoms" => CharacterBottom::class,
        "Tops" => CharacterTop::class,
        "Hats" => CharacterHat::class,
    ];

    private static $summary_fields = [
        "Title" => "Title",
    ];

    private static $field_labels = [
        "Title" => "Title",
    ];

    private static $default_sort = "ID ASC";

    private static $table_name = "UserData";

    private static $singular_name = "UserData";
    private static $plural_name = "UserDatas";

    private static $url_segment = "userdata";

    public function getCMSFields()
    {
        $fields = parent::getCMSFields();
        return $fields;
    }

    public function canView($member = null)
    {
        return true;
    }

    public function canEdit($member = null)
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }

    public function canDelete($member = null)
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }

    public function canCreate($member = null, $context = [])
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }
}
